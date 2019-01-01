using Easeware.Remsng.Entities.Entities;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Street> Streets { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Taxpayer> Taxpayers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lcda>()
                .HasIndex(x => x.LcdaCode)
                .IsUnique(true);

            modelBuilder.Entity<Country>()
                .HasIndex(x => x.CountryCode)
                .IsUnique(true);

            modelBuilder.Entity<State>()
                .HasIndex(x => x.StateCode)
                .IsUnique(true);

            modelBuilder.Entity<Lcda>()
                .HasMany(x => x.Licenses)
                .WithOne(x => x.Lcda)
                .HasForeignKey(x => x.LcdaId);

            modelBuilder.Entity<UserLcda>()
             .HasKey(ky => new { ky.UserId, ky.LcdaId });

            modelBuilder.Entity<UserLcda>()
                .HasOne(x => x.Lcda)
                .WithMany(x => x.UserLcdas)
                .HasForeignKey(x => x.LcdaId);

            modelBuilder.Entity<User>()
                .HasIndex(x => x.email)
                .IsUnique();

            modelBuilder.Entity<UserLcda>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserLcdas)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<State>()
                .HasOne(x => x.Country)
                .WithMany(x => x.States)
                .HasForeignKey(x => x.CountryCode)
                .HasPrincipalKey(x => x.CountryCode);

            modelBuilder.Entity<Lcda>()
                .HasOne(x => x.State)
                .WithMany(x => x.Lcdas)
                .HasForeignKey(x => x.StateCode)
                .HasPrincipalKey(x => x.StateCode);

            modelBuilder.Entity<Ward>()
                .HasIndex(x => x.WardCode)
                .IsUnique();

            modelBuilder.Entity<Ward>()
                .HasOne(x => x.Lcda)
                .WithMany(x => x.Wards)
                .HasForeignKey(x => x.LcdaId);

            modelBuilder.Entity<Address>()
                .HasIndex(x => x.HouseNumber);

            modelBuilder.Entity<Street>()
                .HasIndex(x => x.StreetCode)
                .IsUnique();

            modelBuilder.Entity<Address>()
                .HasOne(x => x.Street)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.StreetId);

            modelBuilder.Entity<Company>()
                .HasIndex(x => x.CompanyCode)
                .IsUnique();

            modelBuilder.Entity<Street>()
                .HasOne(x => x.Ward)
                .WithMany(x => x.Streets)
                .HasForeignKey(x => x.WardId);

            modelBuilder.Entity<Taxpayer>()
                .HasOne(x => x.Address)
                .WithMany(x => x.Taxpayers)
                .HasForeignKey(x => x.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Taxpayer>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Taxpayers)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Company>()
                .HasOne(x => x.Lcda)
                .WithMany(x => x.Companies)
                .HasForeignKey(x => x.LcdaId);
        }
    }
}
