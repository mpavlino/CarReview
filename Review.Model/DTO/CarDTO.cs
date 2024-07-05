using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Review.Model.DTO
{
    public class CarDTO
    {
        public int ID { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public int? BrandID { get; set; }
        public BrandDTO Brand { get; set; }
        public string Engine { get; set; }
        public string EnginePower { get; set; }
        public string Torque { get; set; }
        public string EngineDisplacement { get; set; }
        public int TopSpeed { get; set; }
        public decimal Acceleration { get; set; }
        public DateTime ModelYear { get; set; }
        public string Description { get; set; }
        public int? ReviewerID { get; set; } 
        public ReviewerDTO Reviewer { get; set; }
        public int Rating { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public static Expression<Func<Car, CarDTO>> SelectorExpression { get; } = p => new CarDTO()
        {
            ID = p.ID,
            BrandName = p.Brand.Name,
            Model = p.Model,
            BrandID = p.BrandID,
            Brand = p.Brand.ID == null ? null : new BrandDTO()
            {
                ID = p.Brand.ID,
                Name = p.Brand.Name,
                CountryID = p.Brand.CountryID,
                Country = p.Brand.CountryID == null ? null : new CountryDTO() {
                    ID = p.Brand.Country.ID,
                    Name = p.Brand.Country.Name
                }
            },
            Engine = p.Engine,
            EnginePower = p.EnginePower,
            Torque = p.Torque,
            EngineDisplacement = p.EngineDisplacement,
            TopSpeed = p.TopSpeed,
            Acceleration = p.Acceleration,
            ModelYear = p.ModelYear,
            Description = p.Description,
            ReviewerID = p.ReviewerID,
            Reviewer = p.Reviewer.ID == null ? null : new ReviewerDTO() {
                ID = p.Reviewer.ID,
                FirstName = p.Reviewer.FirstName,
                LastName = p.Reviewer.LastName,
                DateOfBirth = p.Reviewer.DateOfBirth,
                Gender = p.Reviewer.Gender,
                About = p.Reviewer.About
            },
            Rating = p.Rating,
            ImageData = p.ImageData,
            ImageMimeType = p.ImageMimeType
        };
    }
}
