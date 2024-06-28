using Newtonsoft.Json.Linq;
using Review.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model.Interfaces {
    public interface ICarService {
        bool IsCarModelNameUnique( string name );
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<Car> GetCarByIdAsync( int id );
        Task<IEnumerable<Car>> SearchCarsAsync( CarFilterModel filter );
        Task<bool> CreateCarAsync( Car car );
        Task<Car> UpdateCarAsync( int id, Car model );
        Task DeleteCarAsync( int id );
    }
}
