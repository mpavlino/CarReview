using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Review.DAL;

[AttributeUsage( AttributeTargets.Property, AllowMultiple = false )]
public class UniqueBrandNameAttribute : ValidationAttribute {
    private CarManagerDbContext _dbContext;

    public UniqueBrandNameAttribute( CarManagerDbContext dbContext ) {
        _dbContext = dbContext; // Replace with your actual DbContext instance
    }

    public override bool IsValid( object value ) {
        if( value is string brandName ) {
            // Check if there is any brand with the same name in the database
            return !_dbContext.Brands.Any( b => b.Name == brandName );
        }
        // Default to valid if value is null or not a string
        return true;
    }
}
