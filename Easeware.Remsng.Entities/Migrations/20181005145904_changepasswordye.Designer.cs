﻿// <auto-generated />
using System;
using Easeware.Remsng.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Easeware.Remsng.Entities.Migrations
{
    [DbContext(typeof(RemsDbContext))]
    [Migration("20181005145904_changepasswordye")]
    partial class changepasswordye
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.HasKey("Id");

                    b.HasIndex("LcdaCode")
                        .IsUnique()
                        .HasFilter("[LcdaCode] IS NOT NULL");

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
                    b.Property<long>("LcdaId");

                    b.Property<long>("UserId");

                    b.HasKey("LcdaId", "UserId");

                    b.HasIndex("UserId");

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

            modelBuilder.Entity("Easeware.Remsng.Entities.Entities.RemsLicence", b =>
                {
                    b.HasOne("Easeware.Remsng.Entities.Entities.Lcda", "Lcda")
                        .WithMany("Licenses")
                        .HasForeignKey("LcdaId")
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
#pragma warning restore 612, 618
        }
    }
}
