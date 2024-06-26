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
    }
}
