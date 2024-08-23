using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model.DTO {
    public class CarReviewDTO {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ReviewerID { get; set; }
        public ReviewerDTO Reviewer { get; set; }
        public int CarID { get; set; }
        public CarDTO Car { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public static Expression<Func<CarReview, CarReviewDTO>> SelectorExpression { get; } = p => new CarReviewDTO() {
            ID = p.ID,
            Title = p.Title,
            Description = p.Description,
            Rating = p.Rating,
            CreatedOn = p.CreatedOn,
            ReviewerID = p.ReviewerId,
            Reviewer = new ReviewerDTO() {
                ID = p.Reviewer.ID,
                FirstName = p.Reviewer.FirstName,
                LastName = p.Reviewer.LastName
            },
            CarID = p.CarID,
            Car = new CarDTO() {
                ID = p.Car.ID,
                BrandID = p.Car.BrandID,
                Brand = p.Car.Brand.ID == null ? null : new BrandDTO() {
                    ID = p.Car.Brand.ID,
                    Name = p.Car.Brand.Name,
                    CountryID = p.Car.Brand.CountryID,
                    Country = p.Car.Brand.CountryID == null ? null : new CountryDTO() {
                        ID = p.Car.Brand.Country.ID,
                        Name = p.Car.Brand.Country.Name
                    }
                },
                Model = p.Car.Model,
                Generation = p.Car.Generation,
                ModelYear = p.Car.ModelYear,
                Engine = p.Car.Engine
            },
            ImageData = p.ImageData,
            ImageMimeType = p.ImageMimeType
        };
    }

}
