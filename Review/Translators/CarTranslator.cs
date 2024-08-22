using Review.Model;
using Review.Models.Car;

namespace Review.Translators {
    public static class CarTranslator {

        public static Model.Car TranslateViewModelToModel( CarViewModel carViewModel ) {
            var carModel = new Car {
                ID = carViewModel.ID,
                BrandID = carViewModel.BrandID,
                Model = carViewModel.Model,
                Generation = carViewModel.Generation,
                Engine = carViewModel.Engine,
                EnginePower = carViewModel.EnginePower,
                Torque = carViewModel.Torque,
                EngineDisplacement = carViewModel.EngineDisplacement,
                TopSpeed = carViewModel.TopSpeed,
                Acceleration = carViewModel.Acceleration,
                ModelYear = carViewModel.ModelYear,
                Description = carViewModel.Description,
                ReviewerID = carViewModel.ReviewerID,
                Rating = carViewModel.Rating,
                ImageData = carViewModel.ImageData,
                ImageMimeType = carViewModel.ImageMimeType
            };

            return carModel;
        }

        public static Model.CarReview TranslateCarReviewViewModelToModel( CarReviewViewModel carReviewViewModel ) {
            var carReviewModel = new CarReview {
                ID = carReviewViewModel.Id,
                Title = carReviewViewModel.Title,
                Description = carReviewViewModel.Description,
                CreatedOn = carReviewViewModel.CreatedOn,   
                Rating = carReviewViewModel.Rating,
                ReviewerId = carReviewViewModel.ReviewerID,
                CarID = carReviewViewModel.CarID,
                ImageData = carReviewViewModel.ImageData,
                ImageMimeType = carReviewViewModel.ImageMimeType
            };

            return carReviewModel;
        }
    }
}
