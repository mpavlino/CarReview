using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model.DTO {
    public class ImageDTO {
        public int ID { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        // Foreign Key to the CarReview
        public int CarReviewId { get; set; }
        public CarReviewDTO CarReview { get; set; }

        public static Expression<Func<Image, ImageDTO>> SelectorExpression { get; } = p => new ImageDTO() {
            ID = p.ID,
            ImageData = p.ImageData,
            ImageMimeType = p.ImageMimeType,
            CarReviewId = p.CarReviewId,
            CarReview = new CarReviewDTO() {
                ID = p.CarReview.ID,
                Title = p.CarReview.Title,
                Description = p.CarReview.Description,
                CreatedOn = p.CarReview.CreatedOn,
                Rating = p.CarReview.Rating
            },

        };
    }
}
