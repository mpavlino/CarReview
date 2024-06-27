﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Review.Model.Interfaces;

namespace Review.Model {
    [AttributeUsage( AttributeTargets.Property, AllowMultiple = false )]
    public class UniqueBrandNameAttribute : ValidationAttribute {
        protected override ValidationResult IsValid( object value, ValidationContext validationContext ) {
            var service = (IBrandService) validationContext.GetService( typeof( IBrandService ) );
            var brand = (Brand) validationContext.ObjectInstance;

            if( service != null && value is string brandName && brand.ID == 0 ) {
                if( !service.IsBrandNameUnique( brandName ) ) {
                    return new ValidationResult( ErrorMessage ?? "A brand with this name already exists." );
                }
            }
            return ValidationResult.Success;
        }
    }
}
