using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Review.Model;
using Review.Model.DTO;
using Review.Model.Interfaces;
using Review.Models;
using Review.Models.Car;
using Review.Translators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Review.Controllers {
    [Authorize]
    public class CarController : Controller {
        private readonly ICarService _carService;
        private readonly IDropdownService _dropdownService;
        private readonly ILogger<CarController> _logger;

        public string AlertMessage {
            get { return TempData["AlertMessage"].ToString(); }
            set { TempData["AlertMessage"] = value; }
        }

        public CarController( ICarService carService, IDropdownService dropdownService, ILogger<CarController> logger ) {
            _carService = carService;
            _dropdownService = dropdownService;
            _logger = logger;
        }

        public async Task<IActionResult> Index() {
            try {
                ViewBag.PossibleBrands = await _dropdownService.GetBrandsAsync();
                ViewBag.PossibleCountries = await _dropdownService.GetCountriesAsync();
                IEnumerable<Car> cars = await _carService.GetAllCarsAsync();
                return View( cars.ToList() );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while getting all cars." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        public async Task<IActionResult> IndexAjax( CarFilterModel filter ) {
            try {
                var cars = await _carService.SearchCarsAsync( filter );
                return PartialView( "_IndexTable", cars.ToList() );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while searching cars." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        [HttpGet]
        [Authorize( Roles = "Administrator" )]
        public async Task<IActionResult> Create() {
            try {
                //ViewBag.PossibleModels = await _dropdownService.GetModelsAsync( int id ); 
                ViewBag.PossibleBrands = await _dropdownService.GetBrandsAsync();
                ViewBag.PossibleReviewers = await _dropdownService.GetReviewersAsync();

                CarViewModel carView = new CarViewModel();
                return View( carView );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while preparing the create view." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        [HttpPost]
        [Authorize( Roles = "Administrator" )]
        public async Task<IActionResult> Create( CarViewModel carViewModel ) {
            try {
                //ViewBag.PossibleModels = await _dropdownService.GetModelsAsync();
                ViewBag.PossibleBrands = await _dropdownService.GetBrandsAsync();
                ViewBag.PossibleReviewers = await _dropdownService.GetReviewersAsync();

                if( ModelState.IsValid ) {
                    Car carModel = CarTranslator.TranslateViewModelToModel( carViewModel );

                    if( carViewModel.ImageData != null ) {
                        // Convert base64 string to byte array
                        carModel.ImageData = carViewModel.ImageData;
                        carModel.ImageMimeType = carViewModel.ImageMimeType; // Ensure you have the mime type set correctly
                    }

                    bool isCarCreated = await _carService.CreateCarAsync( carModel );
                    return RedirectToAction( nameof( Index ) );
                }

                return View( carViewModel );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while creating a car." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        [HttpGet, ActionName( "Edit" )]
        [Authorize( Roles = "Administrator" )]
        public async Task<IActionResult> Edit( int id ) {
            try {
                //ViewBag.PossibleModels = await _dropdownService.GetModelsAsync();
                ViewBag.PossibleBrands = await _dropdownService.GetBrandsAsync();
                ViewBag.PossibleReviewers = await _dropdownService.GetReviewersAsync();
                var car = await _carService.GetCarByIdAsync( id );
                if( car == null ) {
                    return NotFound();
                }

                var carViewModel = new CarViewModel( car );
                return View( carViewModel );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while preparing the edit view." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage( IFormFile file ) {
            if( file != null && file.Length > 0 ) {
                using( var memoryStream = new MemoryStream() ) {
                    await file.CopyToAsync( memoryStream );
                    var fileData = memoryStream.ToArray();

                    return Json( new { imageData = fileData, mimeType = file.ContentType } );
                }
            }

            return Json( new { error = "File upload failed." } );
        }

        [HttpPost]
        public async Task<IActionResult> UploadImageCarReview( IFormFile data ) {
            var files = HttpContext.Request.Form.Files;
            if( files != null && files.Count > 0 ) {
                var uploadedImages = new List<object>();

                foreach( var file in files ) {
                    if( file.Length > 0 ) {
                        using( var memoryStream = new MemoryStream() ) {
                            await file.CopyToAsync( memoryStream );
                            var fileData = memoryStream.ToArray();

                            uploadedImages.Add( new {
                                imageData = fileData,
                                mimeType = file.ContentType
                            } );
                        }
                    }
                }

                return Json( uploadedImages );
            }

            return Json( new { error = "File upload failed." } );
        }



        [HttpPost, ActionName( "Edit" )]
        [Authorize( Roles = "Administrator" )]
        public async Task<IActionResult> Edit( int id, CarViewModel carViewModel ) {
            try {
                //ViewBag.PossibleModels = await _dropdownService.GetModelsAsync();
                ViewBag.PossibleBrands = await _dropdownService.GetBrandsAsync();
                ViewBag.PossibleReviewers = await _dropdownService.GetReviewersAsync();

                if( ModelState.IsValid ) {
                    Car car = CarTranslator.TranslateViewModelToModel( carViewModel );

                    if( carViewModel.ImageData != null ) {
                        // Convert base64 string to byte array
                        car.ImageData = carViewModel.ImageData;
                        car.ImageMimeType = carViewModel.ImageMimeType; // Ensure you have the mime type set correctly
                    }
                    //car.ImageData = imageData ?? car.ImageData;
                    //car.ImageMimeType = imageMimeType ?? car.ImageMimeType;

                    var updatedCar = await _carService.UpdateCarAsync( id, car );
                    return RedirectToAction( nameof( Index ) );
                }

                return View( carViewModel );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while editing a car." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details( int id ) {
            try {
                var car = await _carService.GetCarByIdAsync( id );
                if( car == null ) {
                    return NotFound();
                }
                return View( car );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while preparing the details view." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        [HttpGet]
        public async Task<IActionResult> Blog( int id ) {
            try {
                var car = await _carService.GetCarByIdAsync( id );
                if( car == null ) {
                    return NotFound();
                }
                return View( car );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while preparing the details view." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        public async Task<IActionResult> Delete( int id ) {
            try {
                await _carService.DeleteCarAsync( id );
                return RedirectToAction( "Index" );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while deleting a car." );
                ModelState.AddModelError( string.Empty, $"Error deleting car: {ex.Message}" );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        public IActionResult GenerateImage() {
            return View();
        }

        #region Car reviews



        [HttpGet]
        public async Task<IActionResult> CreateCarReview( int id ) {
            try {
                ViewBag.PossibleReviewers = await _dropdownService.GetReviewersAsync();

                Car car = await _carService.GetCarByIdAsync( id );
                CarReviewViewModel carReviewViewModel = new CarReviewViewModel( car );
                return View( carReviewViewModel );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while preparing the create view." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarReview( CarReviewViewModel carReviewViewModel ) {
            try {
                ViewBag.PossibleBrands = await _dropdownService.GetBrandsAsync();
                ViewBag.PossibleReviewers = await _dropdownService.GetReviewersAsync();

                if( ModelState.IsValid ) {
                    CarReview carReviewModel = CarTranslator.TranslateCarReviewViewModelToModel( carReviewViewModel );

                    //if( carReviewViewModel.Images != null || carReviewViewModel.Images.Count > 0 ) {
                    //    // Convert base64 string to byte array
                    //    foreach( var image in carReviewViewModel.Images ) {
                    //        carReviewModel.Images.Add( image ); // Ensure you have the mime type set correctly
                    //    }
                    //}

                    if( !string.IsNullOrEmpty( carReviewViewModel.UploadedImages ) ) {
                        var imageStrings = carReviewViewModel.UploadedImages.Split( new[] { ';' }, StringSplitOptions.RemoveEmptyEntries );

                        foreach( var imageString in imageStrings ) {
                            // Extract the Base64 part of the string
                            var base64Data = imageString.Substring( imageString.IndexOf( "," ) + 1 );
                            // Convert Base64 string to byte array
                            byte[] imageBytes = Convert.FromBase64String( base64Data );
                            carReviewModel.Images.Add( new Image {
                                ImageData = imageBytes,
                                CarReviewId = carReviewModel.ID
                            } );
                        }
                    }

                    bool isCarCreated = await _carService.CreateCarReviewAsync( carReviewModel );
                    return RedirectToAction( nameof( Details ), new { id = carReviewViewModel.CarID } );
                }

                return View( carReviewViewModel );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while creating a car." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditCarReview( int id ) {
            try {
                ViewBag.PossibleReviewers = await _dropdownService.GetReviewersAsync();
                var car = await _carService.GetCarReviewByIdAsync( id );
                if( car == null ) {
                    return NotFound();
                }

                var carReviewModel = new CarReviewViewModel( car );
                return View( carReviewModel );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while preparing the edit view." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditCarReview( int id, CarReviewViewModel carReviewViewModel ) {
            try {
                ViewBag.PossibleBrands = await _dropdownService.GetBrandsAsync();
                ViewBag.PossibleReviewers = await _dropdownService.GetReviewersAsync();

                if( ModelState.IsValid ) {
                    CarReview carReview = CarTranslator.TranslateCarReviewViewModelToModel( carReviewViewModel );

                    //if( carReviewViewModel.Images != null || carReviewViewModel.Images.Count > 0 ) {
                    //    // Convert base64 string to byte array
                    //    foreach( var image in carReviewViewModel.Images ) {
                    //        carReview.Images.Add( image ); // Ensure you have the mime type set correctly
                    //    }
                    //}
                    if( !string.IsNullOrEmpty( carReviewViewModel.UploadedImages ) ) {
                        var imageStrings = carReviewViewModel.UploadedImages.Split( new[] { ';' }, StringSplitOptions.RemoveEmptyEntries );

                        foreach( var imageString in imageStrings ) {
                            // Extract the Base64 part of the string
                            var base64Data = imageString.Substring( imageString.IndexOf( "," ) + 1 );
                            // Convert Base64 string to byte array
                            byte[] imageBytes = Convert.FromBase64String( base64Data );
                            carReview.Images.Add( new Image {
                                ImageData = imageBytes,
                                CarReviewId = carReview.ID
                            } );
                        }
                    }

                    var updatedCar = await _carService.UpdateCarReviewAsync( id, carReview );
                    return RedirectToAction( nameof( Details ), new { id = carReviewViewModel.CarID } );
                }

                return View( carReviewViewModel );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while editing a car review." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        public async Task<IActionResult> DeleteCarReview( int id ) {
            try {
                CarReview carReview = await _carService.GetCarReviewByIdAsync( id );
                int carID = carReview.CarID;
                await _carService.DeleteCarReviewAsync( id );
                return RedirectToAction( nameof( Details ), new { id = carID } );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while deleting a car review." );
                ModelState.AddModelError( string.Empty, $"Error deleting car review: {ex.Message}" );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        #endregion

        #region Helper actions

        [HttpGet]
        public async Task<IActionResult> GetModelsByBrand( int brandId ) {
            if( brandId == 0 ) {
                return Json( new List<SelectListItem>() );
            }

            var models = await _dropdownService.GetModelsAsync( brandId );
            return Json( models );
        }

        #endregion
    }
}
