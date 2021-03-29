using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Review.DAL;
using Review.Model;
using Review.Models;

namespace Review.Controllers
{
    
    public class CarController : Controller
    {
      
        private CarManagerDbContext _dbContext;
        private UserManager<AppUser> _userManager;

        public CarController(CarManagerDbContext dbContext, UserManager<AppUser> userManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
        }

        [Authorize]
        public IActionResult Index(string query = null)
        {
            IQueryable<Car> carQuery = this._dbContext.Cars.Include(c => c.Brand).Include(c => c.Country).Include(c => c.Reviewer).AsQueryable();

            //if (!string.IsNullOrWhiteSpace(query))
            //    carQuery = carQuery.Where(p => p.Model.Contains(query));

            return View(carQuery.ToList());
        }


        [HttpPost]
        public ActionResult IndexAjax(CarFilterModel filter)
        {
            var carQuery = this._dbContext.Cars.Include(c => c.Brand).Include(c => c.Country).Include(c => c.Reviewer).AsQueryable();

            //Primjer iterativnog građenja upita - dodaje se "where clause" samo u slučaju da je parametar doista proslijeđen.
            //To rezultira optimalnijim stablom izraza koje se kvalitetnije potencijalno prevodi u SQL
            if (!string.IsNullOrWhiteSpace(filter.Brand))
                carQuery = carQuery.Where(p => p.BrandID != null && p.Brand.Name.Contains(filter.Brand.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Model))
                carQuery = carQuery.Where(p => p.Model.Contains(filter.Model.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Engine))
                carQuery = carQuery.Where(p => p.Engine.Contains(filter.Engine.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Country))
                carQuery = carQuery.Where(p => p.CountryID != null && p.Country.Name.Contains(filter.Country.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Reviewer))
                carQuery = carQuery.Where(p => p.ReviewerID != null && p.Reviewer.FullName.Contains(filter.Reviewer.ToLower()));

            var model = carQuery.ToList();
            return PartialView("_IndexTable", model);
        }

        public IActionResult Details(int? id = null)
        {
            Car car = this._dbContext.Cars
                .Include(p => p.Brand)
                .Include(p => p.Country)
                .Include(p => p.Reviewer)
                .Where(p => p.ID == id)
                .FirstOrDefault();

            return View(car);
        }

        public IActionResult DeleteCar(int? id = null)
        {
            var existing = _dbContext.Cars.FirstOrDefault(p => p.ID == id);
            if (existing != null)
            {
                this._dbContext.Entry(existing).State = EntityState.Deleted;
                this._dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest(new { error = "Unable to locate client with provided ID", providedID = id });
            }
        }

        public IActionResult Blog(int id)
        {
            var car = this._dbContext.Cars
                .Include(p => p.Brand)
                .Include(p => p.Country)
                .Include(p => p.Reviewer)
                .Where(p => p.ID == id)
                .FirstOrDefault();

            return View(car);
        }

        [HttpGet]
        public IActionResult Create()
        {
            this.FillDropdownValues();
            this.FillDropdownValues2();
            this.FillDropdownValues3();
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(Car carmodel)
        {
            if (ModelState.IsValid)
            {
                this._dbContext.Cars.Add(carmodel);
                this.FillDropdownValues();
                this.FillDropdownValues2();
                this.FillDropdownValues3();
                this._dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }


            return View(carmodel);
        }

        [ActionName("Edit")]
        public IActionResult EditGet(int id)
        {
            this.FillDropdownValues();
            this.FillDropdownValues2();
            this.FillDropdownValues3();
            return View(this._dbContext.Cars.FirstOrDefault(p => p.ID == id));
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            var car = this._dbContext.Cars.FirstOrDefault(p => p.ID == id);
            var ok = await this.TryUpdateModelAsync(car);

            if (ok)
            {
                this.FillDropdownValues();
                this.FillDropdownValues2();
                this.FillDropdownValues3();
                this._dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(car);
        }

        private void FillDropdownValues()
        {
            var brands = new List<SelectListItem>();

            //Polje je opcionalno
            var listItem = new SelectListItem();
            listItem.Text = "- select -";
            listItem.Value = "";
            brands.Add(listItem);

            foreach (var brand in _dbContext.Brands)
            {
                brands.Add(new SelectListItem()
                {
                    Value = "" + brand.ID,
                    Text = brand.Name
                });
            }

            ViewBag.PossibleBrands = brands;
        }

        private void FillDropdownValues2()
        {
            var countries = new List<SelectListItem>();

            //Polje je opcionalno
            var listItem = new SelectListItem();
            listItem.Text = "- select -";
            listItem.Value = "";
            countries.Add(listItem);

            foreach (var country in _dbContext.Countries)
            {
                countries.Add(new SelectListItem()
                {
                    Value = "" + country.ID,
                    Text = country.Name
                });
            }

            ViewBag.PossibleCountries = countries;
        }

        private void FillDropdownValues3()
        {

            List<SelectListItem> reviewers = new List<SelectListItem>();

            List<string> Tables = new List<string>();    


            //Polje je opcionalno
            var listItem = new SelectListItem();
            listItem.Text = "- select -";
            listItem.Value = "";
            reviewers.Add(listItem);

            foreach (var reviewer in _dbContext.Reviewers)
            {
                reviewers.Add(new SelectListItem()
                {
                    Value = "" + reviewer.ID,
                    Text = reviewer.FullName
                });
            }

            ViewBag.PossibleReviewers = reviewers;
        }


        // Reviewer Actions

        public IActionResult ReviewerDetails(int? id = null)
        {
            var reviewer = this._dbContext.Reviewers
                .Where(p => p.ID == id)
                .FirstOrDefault();

            return View(reviewer);
        }

        public IActionResult DeleteReviewer(int? id = null)
        {
            var existing = _dbContext.Reviewers.FirstOrDefault(p => p.ID == id);
            var usedReviewer = _dbContext.Cars.Where(o => o.Reviewer.ID == id).FirstOrDefault();

            if (usedReviewer == null)
            {
                if (existing != null)
                {
                    this._dbContext.Entry(existing).State = EntityState.Deleted;
                    this._dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return BadRequest(new { error = "Unable to locate client with provided ID", providedID = id });
                }
            }
            else
            {
                return BadRequest(new { error = "Unable to delete reviewer with provided ID because he is used as a reviewer on a car blog", providedID = id });
            }
        }

        [ActionName("GetReviewers")]
        public IActionResult GetReviewers()
        {
            IQueryable<Reviewer> reviewers = this._dbContext.Reviewers.AsQueryable();
            return View(reviewers.ToList());
        }

        public IActionResult CreateReviewer()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CreateReviewer(Reviewer model)
        {
            if (ModelState.IsValid)
            {
                
                this._dbContext.Reviewers.Add(model);
                this._dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [ActionName("EditReviewer")]
        public IActionResult EditGetReviewer(int id)
        {
            
            return View(this._dbContext.Reviewers.FirstOrDefault(p => p.ID == id));
        }

        [HttpPost, ActionName("EditReviewer")]
        public async Task<IActionResult> EditPostReviewer(int id)
        {
            var reviewer = this._dbContext.Reviewers.FirstOrDefault(p => p.ID == id);
            var ok = await this.TryUpdateModelAsync(reviewer);

            if (ok)
            {
                this._dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(reviewer);
        }
    }
}