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
        public DbSet<UserLcda> UserLcdas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VerificationDetail> VerificationDetails { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Ward> Wards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lcda>()
                .HasIndex(x => x.LcdaCode)
                .IsUnique(true);

            modelBuilder.Entity<Country>()
                .HasIndex(x => x.CountryCode)
                .IsUnique(true);

            modelBuilder.Entity<State>()
                .HasIndex(x => x.stateCode)
                .IsUnique(true);

            modelBuilder.Entity<Lcda>()
                .HasIndex(x => x.LcdaCode)
                .IsUnique(true);

            modelBuilder.Entity<Lcda>()
                .HasMany(x => x.Licenses)
                .WithOne(x => x.Lcda)
                .HasForeignKey(x => x.LcdaId);

            modelBuilder.Entity<UserLcda>()
             .HasKey(ky => new { ky.LcdaId, ky.UserId });

            modelBuilder.Entity<UserLcda>()
                .HasOne(x => x.Lcda)
                .WithMany(x => x.UserLcdas)
                .HasForeignKey(x => x.LcdaId);

            modelBuilder.Entity<UserLcda>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserLcdas)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<User>()
                .HasIndex(x => x.email)
                .IsUnique();

            modelBuilder.Entity<State>()
                .HasOne(x => x.Country)
                .WithMany(x => x.States)
                .HasForeignKey(x => x.countryId);

            modelBuilder.Entity<Lcda>()
                .HasOne(x => x.State)
                .WithMany(x => x.Lcdas)
                .HasForeignKey(x => x.StateId);

            modelBuilder.Entity<Ward>()
                .HasIndex(x => x.WardCode)
                .IsUnique();

            modelBuilder.Entity<Ward>()
                .HasOne(x => x.Lcda)
                .WithMany(x => x.Wards)
                .HasForeignKey(x => x.LcdaId);
        }
    }
}
