using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Review.Model.Interfaces;
using Review.Models;
using Review.Models.Car;

namespace Review.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService _carService;
        private readonly ILogger<CarController> _logger;

        public HomeController(ICarService carService, ILogger<CarController> logger ) {
            _carService = carService;
            _logger = logger;
        }   

        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllCarsAsync();
            var carsViewModelList = new List<CarViewModel>();
            foreach( var car in cars.Where( x => x.ImageData != null ) ) {
                carsViewModelList.Add( new CarViewModel( car ) );
            }
            return View( carsViewModelList );
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
