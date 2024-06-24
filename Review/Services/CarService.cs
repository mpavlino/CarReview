using Review.DAL;
using Review.Model.Interfaces;
using System.Linq;

namespace Review.Services {
    public class CarService : ICarService {
        private CarManagerDbContext _dbContext;

        public CarService( CarManagerDbContext dbContext ) {
            _dbContext = dbContext;
        }

        public bool IsCarModelNameUnique( string name ) {
            return !_dbContext.Cars.Any( b => b.Model == name );
        }
    }
}
