using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model.Interfaces {
    public interface IDropdownService {
        Task<IEnumerable<SelectListItem>> GetCountriesAsync();
        Task<IEnumerable<SelectListItem>> GetBrandsAsync();
        Task<IEnumerable<SelectListItem>> GetReviewersAsync();
    }
}
