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
        public async Task<IActionResult> Create() {
            try {
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
        public async Task<IActionResult> Create( CarViewModel carViewModel ) {
            try {
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
        public async Task<IActionResult> Edit( int id ) {
            try {
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



        [HttpPost, ActionName( "Edit" )]
        public async Task<IActionResult> Edit( int id, CarViewModel carViewModel ) {
            try {
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

        public async Task<IActionResult> DeleteCar( int id ) {
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
    }
}
