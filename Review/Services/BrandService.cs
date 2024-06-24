using Review.DAL;
using Review.Model.Interfaces;
using System.Linq;

namespace Review.Services {
    public class BrandService : IBrandService {
        private CarManagerDbContext _dbContext;

        public BrandService( CarManagerDbContext dbContext ) {
            _dbContext = dbContext;
        }

        public bool IsBrandNameUnique( string name ) {
            return !_dbContext.Brands.Any( b => b.Name == name );
        }
    }


}
