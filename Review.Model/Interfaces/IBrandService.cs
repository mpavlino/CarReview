using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model.Interfaces {
    public interface IBrandService {
        bool IsBrandNameUnique( string name );
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
        Task<Brand> GetBrandByIdAsync( int id );
        Task<Brand> CreateBrandAsync( Brand brand );
        Task<Brand> UpdateBrandAsync( int id, Brand brand );
        Task DeleteBrandAsync( int id );
    }

}
