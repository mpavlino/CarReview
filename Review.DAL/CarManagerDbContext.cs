using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Review.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Review.DAL
{
    public class CarManagerDbContext : IdentityDbContext<AppUser>
    {
        protected CarManagerDbContext()
        {
        }

        public CarManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            PasswordHasher<AppUser> hasher = new PasswordHasher<AppUser>();

            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 1, Name = "Audi", CountryID = 1 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 2, Name = "BMW", CountryID = 1 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 11, Name = "Ferrari", CountryID = 5 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 9, Name = "Ford", CountryID = 4 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 8, Name = "Honda", CountryID = 3 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 12, Name = "Lamborghini", CountryID = 5 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 7, Name = "Mazda", CountryID = 3 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 6, Name = "Mercedes-Benz", CountryID = 1 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 4, Name = "Peugeot", CountryID = 2 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 3, Name = "Renault", CountryID = 2 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 10, Name = "Toyota", CountryID = 3 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 5, Name = "Volkswagen", CountryID = 1 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 13, Name = "McLaren", CountryID = 6 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 14, Name = "Jaguar", CountryID = 6 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 15, Name = "KIA", CountryID = 7 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 16, Name = "Hyundai", CountryID = 7 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 17, Name = "Fiat", CountryID = 5 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 18, Name = "Porsche", CountryID = 1 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 19, Name = "Opel", CountryID = 1 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 20, Name = "Alfa Romeo", CountryID = 5 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 21, Name = "Bugatti", CountryID = 1 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 22, Name = "Aston Martin", CountryID = 6 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 23, Name = "McLaren", CountryID = 6 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 24, Name = "Land Rover", CountryID = 6 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 25, Name = "Citroen", CountryID = 2 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 26, Name = "Volvo", CountryID = 8 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 27, Name = "Jeep", CountryID = 4 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 28, Name = "Nissan", CountryID = 3 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 29, Name = "Bentley", CountryID = 6 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 30, Name = "Škoda", CountryID = 9 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 31, Name = "Tesla", CountryID = 4 } );
            modelBuilder.Entity<Brand>().HasData( new Brand { ID = 32, Name = "Seat", CountryID = 10 } );

            modelBuilder.Entity<Country>().HasData(new Country { ID = 1, Name = "Germany" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 2, Name = "France" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 3, Name = "Japan" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 4, Name = "USA" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 5, Name = "Italy" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 6, Name = "UK" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 7, Name = "South Korea" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 8, Name = "Sweden" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 9, Name = "Czech Republic" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 10, Name = "Spain" });

            modelBuilder.Entity<Reviewer>().HasData(new Reviewer { ID = 1, FirstName = "Branko", LastName = "Marić", Gender = 'M', DateOfBirth = new DateTime( 1980, 10, 10 ) } );
            modelBuilder.Entity<Reviewer>().HasData(new Reviewer { ID = 2, FirstName = "Juraj", LastName = "Šebalj", Gender = 'M', DateOfBirth = new DateTime( 1975, 07, 17 ) } );

            modelBuilder.Entity<Car>().HasData( new Car {
                ID = 1,
                BrandID = 1,
                //Brand = Brands.FirstOrDefault( x => x.ID == 1 ),
                Model = "A3",
                Engine = "2.0 TDI",
                EnginePower = "150",
                Torque = "360",
                EngineDisplacement = "1998",
                TopSpeed = 210,
                Acceleration = 8,
                ModelYear = new DateTime( 2021, 2, 10 ),
                Description = "Compact limousine.",
                //Country = Countries.FirstOrDefault( x => x.ID == 1 ),
                ReviewerID = 1,
                //Reviewer = Reviewers.FirstOrDefault( x => x.ID == 1 )
            } );
            modelBuilder.Entity<Car>().HasData( new Car {
                ID = 2,
                BrandID = 1,
                //Brand = Brands.FirstOrDefault( x => x.ID == 1 ),
                Model = "A6",
                Engine = "3.0 TDI",
                EnginePower = "258",
                Torque = "500",
                EngineDisplacement = "2998",
                TopSpeed = 250,
                Acceleration = 6,
                ModelYear = new DateTime( 2020, 4, 10 ),
                Description = "Compact limousine.",
                //Country = Countries.FirstOrDefault( x => x.ID == 1 ),
                ReviewerID = 2,
                //Reviewer = Reviewers.FirstOrDefault( x => x.ID == 1 )
            } );

            modelBuilder.Entity<AppUser>().HasData( new AppUser { 
                UserName = "korisnik@test.com", 
                Email = "korisnik@test.com",
                NormalizedEmail = "KORISNIK@TEST.COM",
                NormalizedUserName = "KORISNIK@TEST.COM",
                OIB = "84858742558", 
                PasswordHash = hasher.HashPassword( null, "12345M.78" ),
                EmailConfirmed = true,
                SecurityStamp = string.Empty,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            } );
        }

    }
}
