using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Review.DAL
{
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CarManagerDbContext>
{
    public CarManagerDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var builder = new DbContextOptionsBuilder<CarManagerDbContext>();
        var connectionString = configuration.GetConnectionString("CarManagerDbContext");
        builder.UseSqlServer(connectionString);
        return new CarManagerDbContext(builder.Options);
    }
}
}
