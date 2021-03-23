using Microsoft.EntityFrameworkCore;
using Review.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Review.DAL
{
    public class CarManagerDbContext : DbContext
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

            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 1, Name = "Audi" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 2, Name = "BMW" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 11, Name = "Ferrari" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 9, Name = "Ford" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 8, Name = "Honda" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 12, Name = "Lamborghini" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 7, Name = "Mazda" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 6, Name = "Mercedes-Benz" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 4, Name = "Peugeot" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 3, Name = "Renault" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 10, Name = "Toyota" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 5, Name = "Volkswagen" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 13, Name = "McLaren" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 14, Name = "Jaguar" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 15, Name = "KIA" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 16, Name = "Hyundai" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 17, Name = "Fiat" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 18, Name = "Porsche" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 19, Name = "Opel" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 20, Name = "Alfa Romeo" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 21, Name = "Bugatti" });
            modelBuilder.Entity<Brand>().HasData(new Brand { ID = 22, Name = "Aston Martin" });

            modelBuilder.Entity<Country>().HasData(new Country { ID = 1, Name = "Germany" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 2, Name = "France" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 3, Name = "Japan" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 4, Name = "USA" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 5, Name = "Italy" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 6, Name = "UK" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 7, Name = "South Korea" });

            modelBuilder.Entity<Reviewer>().HasData(new Reviewer { ID = 1, FirstName = "Branko", LastName = "Marić" });
            modelBuilder.Entity<Reviewer>().HasData(new Reviewer { ID = 2, FirstName = "Juraj", LastName = "Šebalj" });
        }

    }
}
