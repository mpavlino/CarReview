using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Review.DAL;
using Review.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review.Services {
    public class DropdownService : IDropdownService {
        private readonly CarManagerDbContext _dbContext;

        public DropdownService( CarManagerDbContext dbContext ) {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SelectListItem>> GetModelsAsync( int id ) {
            var models = new List<SelectListItem>
            {
                new SelectListItem { Text = "- select -", Value = "" }
            };

            var modelsList = await _dbContext.Models
                .Where( x => x.BrandId == id )
                .OrderBy( c => c.Name )
                .Select( c => new SelectListItem {
                    Value = c.Id.ToString(),
                    Text = c.Name
                } ).ToListAsync();

            models.AddRange( modelsList );

            return models;
        }

        public async Task<IEnumerable<SelectListItem>> GetCountriesAsync() {
            var countries = new List<SelectListItem>
            {
                new SelectListItem { Text = "- select -", Value = "" }
            };

            var countryList = await _dbContext.Countries
                .OrderBy( c => c.Name )
                .Select( c => new SelectListItem {
                    Value = c.ID.ToString(),
                    Text = c.Name
                } ).ToListAsync();

            countries.AddRange( countryList );

            return countries;
        }

        public async Task<IEnumerable<SelectListItem>> GetBrandsAsync() {
            var brands = new List<SelectListItem>
            {
                new SelectListItem { Text = "- select -", Value = "" }
            };

            var brandList = await _dbContext.Brands
                .OrderBy( c => c.Name )
                .Select( c => new SelectListItem {
                    Value = c.ID.ToString(),
                    Text = c.Name
                } ).ToListAsync();

            brands.AddRange( brandList );

            return brands;
        }

        public async Task<IEnumerable<SelectListItem>> GetReviewersAsync() {
            var reviewers = new List<SelectListItem>
            {
                new SelectListItem { Text = "- select -", Value = "" }
            };

            var reviewerList = await _dbContext.Reviewers
                .OrderBy( c => c.LastName )
                .Select( c => new SelectListItem {
                    Value = c.ID.ToString(),
                    Text = c.FullName
                } ).ToListAsync();

            reviewers.AddRange( reviewerList );

            return reviewers;
        }
    }
}
