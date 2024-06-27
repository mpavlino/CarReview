using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Review.Model;
using Review.Model.DTO;
using Review.Model.Interfaces;
using Review.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
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
                IEnumerable<Car> cars = await _carService.GetAllCarsAsync();
                return View( cars.ToList() );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while getting all cars." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create() {
            try {
                ViewBag.PossibleBrands = await _dropdownService.GetBrandsAsync();
                ViewBag.PossibleReviewers = await _dropdownService.GetReviewersAsync();
                return View();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while preparing the create view." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create( Car carModel ) {
            try {
                ViewBag.PossibleBrands = await _dropdownService.GetBrandsAsync();
                ViewBag.PossibleReviewers = await _dropdownService.GetReviewersAsync();

                if( ModelState.IsValid ) {
                    var createdCar = await _carService.CreateCarAsync( carModel );
                    return RedirectToAction( nameof( Index ) );
                }

                return View( carModel );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while creating a car." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
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
                return View( car );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while preparing the edit view." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
            }
        }

        [HttpPost, ActionName( "Edit" )]
        public async Task<IActionResult> Edit( int id, Car car ) {
            try {
                ViewBag.PossibleBrands = await _dropdownService.GetBrandsAsync();
                ViewBag.PossibleReviewers = await _dropdownService.GetReviewersAsync();

                if( ModelState.IsValid ) {
                    var updatedCar = await _carService.UpdateCarAsync( id, car );
                    return RedirectToAction( nameof( Index ) );
                }

                return View( car );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while editing a car." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
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
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
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
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
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
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
            }
        }
    }
}
