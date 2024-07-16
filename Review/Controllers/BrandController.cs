using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Review.DAL;
using Review.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Review.Model.Interfaces;
using System.Drawing.Drawing2D;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.Extensions.Logging;
using Review.Models;
using System.Diagnostics;
using System;
using Review.Models.Brand;
using Review.Translators;

namespace Review.Controllers {

    [Authorize(Roles = "Administrator")]
    public class BrandController : Controller {

        private readonly IBrandService _brandService;
        private readonly IDropdownService _dropdownService;
        private readonly ILogger<BrandController> _logger;

        public string AlertMessage {
            get { return TempData["AlertMessage"].ToString(); }
            set { TempData["AlertMessage"] = value; }
        }

        public BrandController( IBrandService brandService, IDropdownService dropdownService, ILogger<BrandController> logger ) {
            _brandService = brandService;
            _dropdownService = dropdownService;
            _logger = logger;
        }

        public async Task<IActionResult> Index() {
            try {
                IEnumerable<Brand> brandQuery = await _brandService.GetAllBrandsAsync();
                return View( brandQuery.ToList() );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while getting all brands." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create() {
            try {
                ViewBag.PossibleCountries = await _dropdownService.GetCountriesAsync();
                var brandViewModel = new BrandViewModel();
                return View( brandViewModel );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while preparing the create view." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create( BrandViewModel brandViewModel ) {
            try {
                ViewBag.PossibleCountries = await _dropdownService.GetCountriesAsync();

                if( ModelState.IsValid ) {
                    var brandModel = BrandTranslator.TranslateViewModelToModel( brandViewModel );
                    bool isBrandCreated = await _brandService.CreateBrandAsync( brandModel );
                    return RedirectToAction( nameof( Index ) );
                }
                return View( brandViewModel );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while creating a brand." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        [HttpGet, ActionName( "Edit" )]
        public async Task<IActionResult> Edit( int id ) {
            try {
                ViewBag.PossibleCountries = await _dropdownService.GetCountriesAsync();
                var brand = await _brandService.GetBrandByIdAsync( id );
                if( brand == null ) {
                    return NotFound();
                }
                BrandViewModel brandViewModel = new BrandViewModel(brand);
                return View( brandViewModel );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while preparing the edit view." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        [HttpPost, ActionName( "Edit" )]
        public async Task<IActionResult> Edit( int id, BrandViewModel brandViewModel ) {
            try {
                ViewBag.PossibleCountries = await _dropdownService.GetCountriesAsync();

                if( ModelState.IsValid ) {
                    Brand brand = BrandTranslator.TranslateViewModelToModel( brandViewModel );
                    var updatedBrand = await _brandService.UpdateBrandAsync( id, brand );
                    return RedirectToAction( nameof( Index ) );
                }
                return View( brandViewModel );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while editing a brand." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }

        public async Task<IActionResult> DeleteBrand( int id ) {
            try {
                await _brandService.DeleteBrandAsync( id );
                return RedirectToAction( "Index" );
            }
            catch( HttpRequestException ex ) {
                _logger.LogError( ex, "An error occurred while deleting a brand." );
                ModelState.AddModelError( string.Empty, $"Error deleting brand: {ex.Message}" );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex.Message } );
            }
        }
    }
}
