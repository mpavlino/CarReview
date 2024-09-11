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
        public List<ImageDTO> Images { get; set; }

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
                ModelID = p.Car.ModelID,
                Model = p.Car.ModelID == null ? null : new ModelDTO() {
                    Id = p.Car.Model.Id,
                    Name = p.Car.Model.Name,
                    BrandId = p.Car.Model.Brand.ID
                },
                Generation = p.Car.Generation,
                ModelYearFrom = p.Car.ModelYearFrom,
                ModelYearTo = p.Car.ModelYearTo
            },
            Images = p.Images.Select( r => new ImageDTO {
                ID = r.ID,
                ImageData = r.ImageData,
                ImageMimeType = r.ImageMimeType,
                CarReviewId = r.CarReviewId
            } ).ToList()
        };
    }

}
