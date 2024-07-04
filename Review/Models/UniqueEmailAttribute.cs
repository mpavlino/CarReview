using Review.DAL;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Review.Models {
    public class UniqueEmailAttribute : ValidationAttribute {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext ) {
            var _dbContext = (CarManagerDbContext) validationContext.GetService( typeof( CarManagerDbContext ) );
            var entity = _dbContext.Users.SingleOrDefault( e => e.Email == value.ToString() );

            if( entity != null ) {
                return new ValidationResult( GetErrorMessage( value.ToString() ) );
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage( string email ) {
            return $"Email {email} is already in use.";
        }
    }
}
