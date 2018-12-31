﻿// <auto-generated />
using System;
using Easeware.Remsng.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Easeware.Remsng.Entities.Migrations
{
    [DbContext(typeof(RemsDbContext))]
    partial class RemsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<long>("OwnerId");

                    b.Property<string>("StreetCode");

                    b.HasKey("Id");

                    b.HasIndex("HouseNumber");

                    b.HasIndex("OwnerId");

                    b.HasIndex("StreetCode");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyCode")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("LcdaCode")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CompanyCode")
                        .IsUnique()
                        .HasFilter("[CompanyCode] IS NOT NULL");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.Country", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("CountryCode")
                        .IsUnique();

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.IssuedLicense", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Lcda")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LicenseValue")
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("IssuedLicenses");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.Lcda", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("LcdaCode")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LcdaName")
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("LcdaStatus");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("StateCode");

                    b.HasKey("Id");

                    b.HasIndex("LcdaCode")
                        .IsUnique()
                        .HasFilter("[LcdaCode] IS NOT NULL");

                    b.HasIndex("StateCode");

                    b.ToTable("Lcdas");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.RemsLicence", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<long>("LcdaId");

                    b.Property<string>("LicenseStatus")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LicenseValue")
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<int>("UsageCount");

                    b.HasKey("Id");

                    b.HasIndex("LcdaId");

                    b.ToTable("Licences");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.Sector", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("LcdaCode");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("SectorCode")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("SectorName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SectorStatus");

                    b.HasKey("Id");

                    b.ToTable("Sectors");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.State", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("StateCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("StateName")
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("CountryCode");

                    b.HasIndex("StateCode")
                        .IsUnique();

                    b.ToTable("States");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.Street", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("StreetCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("StreetName")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("StreetStatus")
                        .HasColumnType("nvarchar(20)");

                    b.Property<long>("WardId");

                    b.HasKey("Id");

                    b.HasIndex("StreetCode")
                        .IsUnique();

                    b.HasIndex("WardId");

                    b.ToTable("Streets");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.Taxpayer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressCode");

                    b.Property<string>("CompanyCode");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("OtherNames")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AddressCode")
                        .IsUnique()
                        .HasFilter("[AddressCode] IS NOT NULL");

                    b.HasIndex("CompanyCode")
                        .IsUnique()
                        .HasFilter("[CompanyCode] IS NOT NULL");

                    b.ToTable("Taxpayers");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.User", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTimeOffset?>("lockedOutEndDateUTC");

                    b.Property<int>("lockedoutCount");

                    b.Property<bool>("lockedoutenabled");

                    b.Property<string>("otherNames")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("passwordHash")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("securityStamp");

                    b.Property<string>("userStatus");

                    b.HasKey("id");

                    b.HasIndex("email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.UserLcda", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("LcdaId");

                    b.HasKey("UserId", "LcdaId");

                    b.HasIndex("LcdaId");

                    b.ToTable("UserLcdas");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.VerificationDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<bool>("IsVerified");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VerificationCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("VerificationDetails");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.Ward", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<long>("LcdaId");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("Status");

                    b.Property<string>("WardCode")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("WardName")
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("LcdaId");

                    b.HasIndex("WardCode")
                        .IsUnique()
                        .HasFilter("[WardCode] IS NOT NULL");

                    b.ToTable("Wards");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.Address", b =>
                {
                    b.HasOne("Easeware.Remsng.Entities.Entities.Street", "Street")
                        .WithMany("Addresses")
                        .HasForeignKey("StreetCode")
                        .HasPrincipalKey("StreetCode");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.Lcda", b =>
                {
                    b.HasOne("Easeware.Remsng.Entities.Entities.State", "State")
                        .WithMany("Lcdas")
                        .HasForeignKey("StateCode")
                        .HasPrincipalKey("StateCode");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.RemsLicence", b =>
                {
                    b.HasOne("Easeware.Remsng.Entities.Entities.Lcda", "Lcda")
                        .WithMany("Licenses")
                        .HasForeignKey("LcdaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.State", b =>
                {
                    b.HasOne("Easeware.Remsng.Entities.Entities.Country", "Country")
                        .WithMany("States")
                        .HasForeignKey("CountryCode")
                        .HasPrincipalKey("CountryCode");
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.Street", b =>
                {
                    b.HasOne("Easeware.Remsng.Entities.Entities.Ward", "Ward")
                        .WithMany("Streets")
                        .HasForeignKey("WardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.UserLcda", b =>
                {
                    b.HasOne("Easeware.Remsng.Entities.Entities.Lcda", "Lcda")
                        .WithMany("UserLcdas")
                        .HasForeignKey("LcdaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Easeware.Remsng.Entities.Entities.User", "User")
                        .WithMany("UserLcdas")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.Ward", b =>
                {
                    b.HasOne("Easeware.Remsng.Entities.Entities.Lcda", "Lcda")
                        .WithMany("Wards")
                        .HasForeignKey("LcdaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
