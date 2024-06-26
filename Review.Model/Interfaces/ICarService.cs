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
        Task<IEnumerable<CarDTO>> GetAllCarsAsync();
        Task<CarDTO> GetCarByIdAsync( int id );
        Task<IEnumerable<CarDTO>> SearchCarsAsync( string q );
        Task<CarDTO> CreateCarAsync( Car car );
        Task<CarDTO> UpdateCarAsync( int id, JObject model );
        Task DeleteCarAsync( int id );
    }
}
