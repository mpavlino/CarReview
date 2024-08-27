using Microsoft.VisualStudio.TextTemplating;
using Review.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Review.Models.Car {
    public class CarReviewViewModel {

        public CarReviewViewModel() { }

        public CarReviewViewModel( Model.Car car ) { 
            CarName = String.Format( "{0} {1}", car.Brand.Name, car.Model );
            CarID = car.ID;
            CreatedOn = DateTime.Now;
        }
        public CarReviewViewModel( CarReview carReview ) {
            Id = carReview.ID;
            Title = carReview.Title;
            Description = carReview.Description;
            CreatedOn = carReview.CreatedOn;
            Rating = carReview.Rating;
            CarID = carReview.CarID;
            CarName = String.Format( "{0} {1}", carReview.Car.Brand.Name, carReview.Car.Model );
            ReviewerID = carReview.ReviewerId;
            Images = carReview.Images;
            //if( carReview.Images != null || carReview.Images.Count > 0 ) {
            //    Images = carReview.Images;
            //    //foreach( var image in carReview.Images ) {
            //    //    Images.Add( image );
            //    //}
            //}
        }

        public int Id { get; set; }

        [Required( ErrorMessage = "Title is required." )]
        [StringLength( 200 )]
        public string Title { get; set; }
        public string Description { get; set; }

        [DataType( DataType.Date )]
        [DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true )]
        public DateTime CreatedOn { get; set; }
        public int Rating { get; set; }
        public int ReviewerID { get; set; }
        public int CarID { get; set; }
        public string CarName { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
        public string UploadedImages { get; set; }
    }
}
