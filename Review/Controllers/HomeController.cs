using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Review.Model;
using Review.Model.Interfaces;
using Review.Models;
using Review.Models.Car;

namespace Review.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService _carService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ICarService carService, SignInManager<AppUser> signInManager, ILogger<HomeController> logger ) {
            _carService = carService;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index( string query = null ) {
            var carsViewModelList = new List<CarViewModel>();
            IEnumerable<Car> cars = new List<Car>();
            if( _signInManager.IsSignedIn( User ) ) {
                if( query == null ) {
                    cars = await _carService.GetAllCarsAsync();
                }
                else {
                    cars = await _carService.SearchCarsByTextAsync( query );
                }
                cars = cars.OrderBy( x => x.Brand?.Name ).ThenBy( x => x.Model );
                foreach( var car in cars.Where( x => x.ImageData != null ) ) {
                    carsViewModelList.Add( new CarViewModel( car ) );
                }
            }
            return View( carsViewModelList );
        }

        public async Task<IActionResult> TestCarSync() {
            var test = await _carService.SyncCarsAsync();
            return RedirectToAction( "Index" );    
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
