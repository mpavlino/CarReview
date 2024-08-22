using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Review.Model.Interfaces;
using Review.Models.Car;

namespace Review.Validation {
    [AttributeUsage( AttributeTargets.Property, AllowMultiple = false )]
    public class UniqueCarGenerationNameAttribute : ValidationAttribute {
        protected override ValidationResult IsValid( object value, ValidationContext validationContext ) {
            var service = (ICarService) validationContext.GetService( typeof( ICarService ) );
            var car = (CarViewModel) validationContext.ObjectInstance;

            if( service != null && value is string carModelName && car.ID == 0 ) {
                if( !service.IsCarGenerationNameUnique( carModelName ) ) {
                    return new ValidationResult( ErrorMessage ?? "A car with this generation name already exists." );
                }
            }
            return ValidationResult.Success;
        }
    }
}
