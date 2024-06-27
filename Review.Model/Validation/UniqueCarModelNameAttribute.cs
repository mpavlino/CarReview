﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Review.Model.Interfaces;

namespace Review.Model {
    [AttributeUsage( AttributeTargets.Property, AllowMultiple = false )]
    public class UniqueCarModelNameAttribute : ValidationAttribute {
        protected override ValidationResult IsValid( object value, ValidationContext validationContext ) {
            var service = (ICarService) validationContext.GetService( typeof( ICarService ) );
            var car = (Car) validationContext.ObjectInstance;

            if( service != null && value is string carModelName && car.ID == 0 ) {
                if( !service.IsCarModelNameUnique( carModelName ) ) {
                    return new ValidationResult( ErrorMessage ?? "A car with this model name already exists." );
                }
            }
            return ValidationResult.Success;
        }
    }
}
