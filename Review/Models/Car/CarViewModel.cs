using Microsoft.AspNetCore.Http;
using Review.Model;
using Review.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Review.Models.Car {
    public class CarViewModel {

        public CarViewModel() { }
        public CarViewModel( Model.Car car ) {
            ID = car.ID;
            BrandID = car.BrandID;
            BrandName = car.Brand?.Name;
            Model = car.Model;
            Generation = car.Generation;
            Engine = car.Engine;
            EnginePower = car.EnginePower;
            Torque = car.Torque;
            EngineDisplacement = car.EngineDisplacement;
            TopSpeed = car.TopSpeed;
            Acceleration = car.Acceleration;
            ModelYear = car.ModelYear;
            Description = car.Description;
            ReviewerID = car.ReviewerID;
            Rating = car.Rating;
            ImageData = car.ImageData;
            ImageMimeType = car.ImageMimeType;
        }


        public int ID { get; set; }
        public int? BrandID { get; set; }
        public string BrandName { get; set; }
        [Required( ErrorMessage = "Car name is required." )]
        [UniqueCarModelName( ErrorMessage = "A car with this model name already exists." )]
        [StringLength( 200 )]
        public string Model { get; set; }
        public string Generation { get; set; }
        public string Engine { get; set; }
        public string EnginePower { get; set; }
        public string Torque { get; set; }
        public string EngineDisplacement { get; set; }
        public int? TopSpeed { get; set; }
        public decimal? Acceleration { get; set; }
        [DataType( DataType.Date )]
        [DisplayFormat( DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true )]
        public DateTime ModelYear { get; set; }
        public string Description { get; set; }
        public int? ReviewerID { get; set; }
        public int Rating { get; set; }
        public IFormFile ImageFile { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
