using Easeware.Remsng.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Entities
{
    public class RemsDbContext : DbContext
    {
        public RemsDbContext(DbContextOptions<RemsDbContext> options) : base(options)
        {
        }

        public DbSet<Lcda> Lcdas { get; set; }
        public DbSet<RemsLicence> Licences { get; set; }
        public DbSet<IssuedLicense> IssuedLicenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lcda>()
                .HasIndex(x => x.LcdaCode)
                .IsUnique(true);

            modelBuilder.Entity<Lcda>()
                .HasMany(x => x.Licenses)
                .WithOne(x => x.Lcda)
                .HasForeignKey(x => x.LcdaId);
        }
    }
}
