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
        bool IsCarGenerationNameUnique( string name );
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<Car> GetCarByIdAsync( int id );
        Task<IEnumerable<Car>> SearchCarsAsync( CarFilterModel filter );
        Task<IEnumerable<Car>> SearchCarsByTextAsync( string query );
        Task<bool> CreateCarAsync( Car car );
        Task<Car> UpdateCarAsync( int id, Car model );
        Task DeleteCarAsync( int id );
        Task<List<CarReview>> GetCarReviewsByCarIdAsync( int id );
        Task<CarReview> GetCarReviewByIdAsync( int id );
        Task<bool> CreateCarReviewAsync( CarReview carReview );
        Task<CarReview> UpdateCarReviewAsync( int id, CarReview model );
        Task DeleteCarReviewAsync( int id );
    }
}
