﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Review.DAL;

#nullable disable

namespace Review.DAL.Migrations
{
    [DbContext(typeof(CarManagerDbContext))]
    [Migration("20240628073949_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Review.Model.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("OIB")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "c72bc749-8ed2-437d-bac5-2099a7b1428f",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b90651eb-b5ad-416c-bf43-f896db0fc599",
                            Email = "korisnik@test.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "KORISNIK@TEST.COM",
                            NormalizedUserName = "KORISNIK@TEST.COM",
                            OIB = "84858742558",
                            PasswordHash = "AQAAAAIAAYagAAAAEFairTdGPojK550C/Gdwrh3lgYT6qD7bXXLXDgFcb3KavXasbUE2+2cj84XW/UlUmg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "korisnik@test.com"
                        });
                });

            modelBuilder.Entity("Review.Model.Brand", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("CountryID")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CountryID = 1,
                            Name = "Audi"
                        },
                        new
                        {
                            ID = 2,
                            CountryID = 1,
                            Name = "BMW"
                        },
                        new
                        {
                            ID = 11,
                            CountryID = 5,
                            Name = "Ferrari"
                        },
                        new
                        {
                            ID = 9,
                            CountryID = 4,
                            Name = "Ford"
                        },
                        new
                        {
                            ID = 8,
                            CountryID = 3,
                            Name = "Honda"
                        },
                        new
                        {
                            ID = 12,
                            CountryID = 5,
                            Name = "Lamborghini"
                        },
                        new
                        {
                            ID = 7,
                            CountryID = 3,
                            Name = "Mazda"
                        },
                        new
                        {
                            ID = 6,
                            CountryID = 1,
                            Name = "Mercedes-Benz"
                        },
                        new
                        {
                            ID = 4,
                            CountryID = 2,
                            Name = "Peugeot"
                        },
                        new
                        {
                            ID = 3,
                            CountryID = 2,
                            Name = "Renault"
                        },
                        new
                        {
                            ID = 10,
                            CountryID = 3,
                            Name = "Toyota"
                        },
                        new
                        {
                            ID = 5,
                            CountryID = 1,
                            Name = "Volkswagen"
                        },
                        new
                        {
                            ID = 13,
                            CountryID = 6,
                            Name = "McLaren"
                        },
                        new
                        {
                            ID = 14,
                            CountryID = 6,
                            Name = "Jaguar"
                        },
                        new
                        {
                            ID = 15,
                            CountryID = 7,
                            Name = "KIA"
                        },
                        new
                        {
                            ID = 16,
                            CountryID = 7,
                            Name = "Hyundai"
                        },
                        new
                        {
                            ID = 17,
                            CountryID = 5,
                            Name = "Fiat"
                        },
                        new
                        {
                            ID = 18,
                            CountryID = 1,
                            Name = "Porsche"
                        },
                        new
                        {
                            ID = 19,
                            CountryID = 1,
                            Name = "Opel"
                        },
                        new
                        {
                            ID = 20,
                            CountryID = 5,
                            Name = "Alfa Romeo"
                        },
                        new
                        {
                            ID = 21,
                            CountryID = 1,
                            Name = "Bugatti"
                        },
                        new
                        {
                            ID = 22,
                            CountryID = 6,
                            Name = "Aston Martin"
                        },
                        new
                        {
                            ID = 23,
                            CountryID = 6,
                            Name = "McLaren"
                        },
                        new
                        {
                            ID = 24,
                            CountryID = 6,
                            Name = "Land Rover"
                        },
                        new
                        {
                            ID = 25,
                            CountryID = 2,
                            Name = "Citroen"
                        },
                        new
                        {
                            ID = 26,
                            CountryID = 8,
                            Name = "Volvo"
                        },
                        new
                        {
                            ID = 27,
                            CountryID = 4,
                            Name = "Jeep"
                        },
                        new
                        {
                            ID = 28,
                            CountryID = 3,
                            Name = "Nissan"
                        },
                        new
                        {
                            ID = 29,
                            CountryID = 6,
                            Name = "Bentley"
                        },
                        new
                        {
                            ID = 30,
                            CountryID = 9,
                            Name = "Škoda"
                        },
                        new
                        {
                            ID = 31,
                            CountryID = 4,
                            Name = "Tesla"
                        },
                        new
                        {
                            ID = 32,
                            CountryID = 10,
                            Name = "Seat"
                        });
                });

            modelBuilder.Entity("Review.Model.Car", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<decimal>("Acceleration")
                        .HasColumnType("numeric");

                    b.Property<int?>("BrandID")
                        .HasColumnType("integer");

                    b.Property<int?>("CountryID")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Engine")
                        .HasColumnType("text");

                    b.Property<string>("EngineDisplacement")
                        .HasColumnType("text");

                    b.Property<string>("EnginePower")
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("ModelYear")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<int?>("ReviewerID")
                        .HasColumnType("integer");

                    b.Property<int>("TopSpeed")
                        .HasColumnType("integer");

                    b.Property<string>("Torque")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("BrandID");

                    b.HasIndex("CountryID");

                    b.HasIndex("ReviewerID");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Review.Model.Country", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Germany"
                        },
                        new
                        {
                            ID = 2,
                            Name = "France"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Japan"
                        },
                        new
                        {
                            ID = 4,
                            Name = "USA"
                        },
                        new
                        {
                            ID = 5,
                            Name = "Italy"
                        },
                        new
                        {
                            ID = 6,
                            Name = "UK"
                        },
                        new
                        {
                            ID = 7,
                            Name = "South Korea"
                        },
                        new
                        {
                            ID = 8,
                            Name = "Sweden"
                        },
                        new
                        {
                            ID = 9,
                            Name = "Czech Republic"
                        },
                        new
                        {
                            ID = 10,
                            Name = "Spain"
                        },
                        new
                        {
                            ID = 11,
                            Name = "Croatia"
                        });
                });

            modelBuilder.Entity("Review.Model.Reviewer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("About")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<char>("Gender")
                        .HasColumnType("character(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("ID");

                    b.ToTable("Reviewers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Review.Model.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Review.Model.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Review.Model.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Review.Model.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Review.Model.Brand", b =>
                {
                    b.HasOne("Review.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Review.Model.Car", b =>
                {
                    b.HasOne("Review.Model.Brand", "Brand")
                        .WithMany("Cars")
                        .HasForeignKey("BrandID");

                    b.HasOne("Review.Model.Country", null)
                        .WithMany("Cars")
                        .HasForeignKey("CountryID");

                    b.HasOne("Review.Model.Reviewer", "Reviewer")
                        .WithMany("Cars")
                        .HasForeignKey("ReviewerID");

                    b.Navigation("Brand");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("Review.Model.Brand", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("Review.Model.Country", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("Review.Model.Reviewer", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
