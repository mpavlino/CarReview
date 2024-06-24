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

namespace Review.Controllers {

    [Authorize]
    public class BrandController : Controller {

        private CarManagerDbContext _dbContext;
        private UserManager<AppUser> _userManager;
        public string AlertMessage {
            get { return TempData["AlertMessage"].ToString(); }

            set { TempData["AlertMessage"] = value; }
        }

        public BrandController( CarManagerDbContext dbContext, UserManager<AppUser> userManager ) {
            this._dbContext = dbContext;
            this._userManager = userManager;
        }

        public IActionResult Index() {

            IQueryable<Brand> brandQuery = this._dbContext.Brands.Include( c => c.Country ).AsQueryable();

            return View( brandQuery.ToList() );
        }

        [HttpGet]
        public IActionResult Create() {
            this.FillDropdownValues();
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost( Brand brandmodel ) {

            this.FillDropdownValues();

            if( ModelState.IsValid ) {
                this._dbContext.Brands.Add( brandmodel );
                this._dbContext.SaveChanges();

                return RedirectToAction( nameof( Index ) );
            }

            return View( "Create", brandmodel );
        }

        [ActionName( "Edit" )]
        public IActionResult EditGet( int id ) {
            this.FillDropdownValues();
            return View( this._dbContext.Brands.FirstOrDefault( p => p.ID == id ) );
        }

        [HttpPost, ActionName( "Edit" )]
        public async Task<IActionResult> EditPost( int id ) {
            var brand = this._dbContext.Brands.FirstOrDefault( p => p.ID == id );
            var ok = await this.TryUpdateModelAsync( brand );
            this.FillDropdownValues();

            if( ok ) {
                this._dbContext.SaveChanges();
                return RedirectToAction( nameof( Index ) );
            }

            return View( "Edit", brand );
        }

        public IActionResult DeleteBrand( int? id = null ) {
            var existing = _dbContext.Brands.FirstOrDefault( p => p.ID == id );
            if( existing != null ) {
                this._dbContext.Entry( existing ).State = EntityState.Deleted;
                this._dbContext.SaveChanges();
                return RedirectToAction( nameof( Index ) );
            }
            else {
                return BadRequest( new { error = "Unable to locate brand with provided ID", providedID = id } );
            }
        }

        private void FillDropdownValues() {
            var countries = new List<SelectListItem>();

            //Polje je opcionalno
            var listItem = new SelectListItem();
            listItem.Text = "- select -";
            listItem.Value = "";
            countries.Add( listItem );

            foreach( var country in _dbContext.Countries ) {
                countries.Add( new SelectListItem() {
                    Value = "" + country.ID,
                    Text = country.Name
                } );
            }

            ViewBag.PossibleCountries = countries.OrderBy( x => x.Text );
        }
    }
}
