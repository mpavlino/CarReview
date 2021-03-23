﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Review.DAL;

namespace Review.DAL.Migrations
{
    [DbContext(typeof(CarManagerDbContext))]
    [Migration("20210322150006_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Review.Model.Brand", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Audi"
                        },
                        new
                        {
                            ID = 2,
                            Name = "BMW"
                        },
                        new
                        {
                            ID = 11,
                            Name = "Ferrari"
                        },
                        new
                        {
                            ID = 9,
                            Name = "Ford"
                        },
                        new
                        {
                            ID = 8,
                            Name = "Honda"
                        },
                        new
                        {
                            ID = 12,
                            Name = "Lamborghini"
                        },
                        new
                        {
                            ID = 7,
                            Name = "Mazda"
                        },
                        new
                        {
                            ID = 6,
                            Name = "Mercedes-Benz"
                        },
                        new
                        {
                            ID = 4,
                            Name = "Peugeot"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Renault"
                        },
                        new
                        {
                            ID = 10,
                            Name = "Toyota"
                        },
                        new
                        {
                            ID = 5,
                            Name = "Volkswagen"
                        },
                        new
                        {
                            ID = 13,
                            Name = "McLaren"
                        },
                        new
                        {
                            ID = 14,
                            Name = "Jaguar"
                        },
                        new
                        {
                            ID = 15,
                            Name = "KIA"
                        },
                        new
                        {
                            ID = 16,
                            Name = "Hyundai"
                        },
                        new
                        {
                            ID = 17,
                            Name = "Fiat"
                        },
                        new
                        {
                            ID = 18,
                            Name = "Porsche"
                        },
                        new
                        {
                            ID = 19,
                            Name = "Opel"
                        },
                        new
                        {
                            ID = 20,
                            Name = "Alfa Romeo"
                        },
                        new
                        {
                            ID = 21,
                            Name = "Bugatti"
                        },
                        new
                        {
                            ID = 22,
                            Name = "Aston Martin"
                        });
                });

            modelBuilder.Entity("Review.Model.Car", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Acceleration")
                        .HasColumnType("float");

                    b.Property<int?>("BrandID")
                        .HasColumnType("int");

                    b.Property<int?>("CountryID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Engine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("ModelYear")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ReviewerID")
                        .HasColumnType("int");

                    b.Property<int>("TopSpeed")
                        .HasColumnType("int");

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
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

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
                        });
                });

            modelBuilder.Entity("Review.Model.Reviewer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Reviewers");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Branko",
                            Gender = " ",
                            LastName = "Marić"
                        },
                        new
                        {
                            ID = 2,
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Juraj",
                            Gender = " ",
                            LastName = "Šebalj"
                        });
                });

            modelBuilder.Entity("Review.Model.Car", b =>
                {
                    b.HasOne("Review.Model.Brand", "Brand")
                        .WithMany("Cars")
                        .HasForeignKey("BrandID");

                    b.HasOne("Review.Model.Country", "Country")
                        .WithMany("Cars")
                        .HasForeignKey("CountryID");

                    b.HasOne("Review.Model.Reviewer", "Reviewer")
                        .WithMany("Cars")
                        .HasForeignKey("ReviewerID");

                    b.Navigation("Brand");

                    b.Navigation("Country");

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
