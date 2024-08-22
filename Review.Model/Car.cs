using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Review.Model
{
    public class Car
    {
        public int ID { get; set; }

        [ForeignKey(nameof(Brand))]
        public int? BrandID { get; set; }
        public Brand Brand { get; set; }

        [Required( ErrorMessage = "Car name is required." )]
        [StringLength(200)]
        public string Model { get; set; }

        public string Generation { get; set; }

        public string Engine { get; set; }
        public string EnginePower { get; set; }
        public string Torque { get; set; }
        public string EngineDisplacement { get; set; }
        public int? TopSpeed { get; set; }
        public decimal? Acceleration { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ModelYear { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(Reviewer))]
        public int? ReviewerID { get; set; }
        public Reviewer Reviewer { get; set; }
        public int Rating { get; set; }
        public byte[] ImageData { get; set; } 
        public string ImageMimeType { get; set; }
        public List<CarReview> CarReviews { get; set; }
    }
}
