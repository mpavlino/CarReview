using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Review.Model.DTO {
    public class CarDTO {
        public int ID { get; set; }
        public string BrandName { get; set; }
        public int? ModelID { get; set; }
        public ModelDTO Model { get; set; }
        public string Generation { get; set; }
        public int? BrandID { get; set; }
        public BrandDTO Brand { get; set; }
        public DateTime ModelYearFrom { get; set; }
        public DateTime? ModelYearTo { get; set; }
        public string Description { get; set; }
        public int? ReviewerID { get; set; }
        public ReviewerDTO Reviewer { get; set; }
        public int Rating { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public List<CarReviewDTO> CarReviews { get; set; }

        // New property for engines
        public List<EngineDTO> Engines { get; set; }

        public static Expression<Func<Car, CarDTO>> SelectorExpression { get; } = p => new CarDTO() {
            ID = p.ID,
            BrandName = p.Brand.Name,
            ModelID = p.ModelID,
            Model = p.Model.Id == null ? null : new ModelDTO {
                Id = p.Model.Id,
                Name = p.Model.Name,
                BrandId = p.Model.BrandId
            },
            Generation = p.Generation,
            BrandID = p.BrandID,
            Brand = p.Brand.ID == null ? null : new BrandDTO() {
                ID = p.Brand.ID,
                Name = p.Brand.Name,
                CountryID = p.Brand.CountryID,
                Country = p.Brand.CountryID == null ? null : new CountryDTO() {
                    ID = p.Brand.Country.ID,
                    Name = p.Brand.Country.Name
                }
            },
            ModelYearFrom = p.ModelYearFrom, // Adjusted for proper handling
            ModelYearTo = p.ModelYearTo, // Adjusted for proper handling
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
            ImageMimeType = p.ImageMimeType,
            CarReviews = p.CarReviews.Select( r => new CarReviewDTO {
                ID = r.ID,
                Title = r.Title,
                Description = r.Description,
                Rating = r.Rating,
                CreatedOn = r.CreatedOn,
                Reviewer = new ReviewerDTO {
                    ID = r.Reviewer.ID,
                    FirstName = r.Reviewer.FirstName,
                    LastName = r.Reviewer.LastName
                },
                Images = r.Images.Select( i => new ImageDTO {
                    ID = i.ID,
                    ImageData = i.ImageData,
                    ImageMimeType = i.ImageMimeType
                } ).ToList()
            } ).ToList(),
            // Mapping Engines
            Engines = p.Engines.Select( e => new EngineDTO {
                ID = e.ID,
                Cylinders = e.Cylinders,
                Displacement = e.Displacement,
                Power = e.Power,
                Torque = e.Torque,
                FuelSystem = e.FuelSystem,
                FuelType = e.FuelType,
                FuelCapacity = e.FuelCapacity,
                TopSpeed = e.TopSpeed,
                Acceleration = e.Acceleration,
                DriveType = e.DriveType,
                Gearbox = e.Gearbox,
                FrontBrakes = e.FrontBrakes,
                RearBrakes = e.RearBrakes,
                TireSize = e.TireSize,
                Length = e.Length,
                Width = e.Width,
                Height = e.Height,
                FrontRearTrack = e.FrontRearTrack,
                Wheelbase = e.Wheelbase,
                GroundClearance = e.GroundClearance,
                CargoVolume = e.CargoVolume,
                UnladenWeight = e.UnladenWeight,
                GrossWeightLimit = e.GrossWeightLimit,
                FuelEconomyCity = e.FuelEconomyCity,
                FuelEconomyHighway = e.FuelEconomyHighway,
                FuelEconomyCombined = e.FuelEconomyCombined,
                CO2Emissions = e.CO2Emissions
            } ).ToList()
        };
    }
}
