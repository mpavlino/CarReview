using Newtonsoft.Json.Linq;
using Review.DAL;
using Review.Model;
using Review.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Review.Handlers;
using Review.Model.DTO;

namespace Review.Services {
    public class BrandService : BaseService, IBrandService {

        private readonly CarManagerDbContext _dbContext;
        private readonly ILogger<BrandService> _logger;

        public BrandService( CarManagerDbContext dbContext, HttpClient httpClient, TokenHandler tokenHandler, ILogger<BrandService> logger, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor )
            : base( httpClient, userManager, httpContextAccessor, tokenHandler ) {
            _dbContext = dbContext;
            _logger = logger;
        }

        public bool IsBrandNameUnique( string name ) {
            try {
                return !_dbContext.Brands.Any( b => b.Name == name );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while checking brand name uniqueness." );
                throw;
            }
        }

        public async Task<bool> SyncBrandsAsync() {
            try {
                // Fetch brands from the external API
                IEnumerable<Brand> externalBrands = await GetAllBrandsFromApiAsync(); // This calls the external API

                // Prepare the data for the sync API
                var syncData = externalBrands.Select( brand => new Brand {
                    ID = brand.ID,
                    Name = brand.Name,
                    CountryID = null // Adjust if needed
                } ).ToList();

                // Call your API controller's sync endpoint
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.PostAsJsonAsync( "api/brands/sync", syncData );

                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch( Exception ex ) {
                _logger.LogError( ex, ex.InnerException?.Message );
                throw;
            }
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsFromApiAsync() {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetAsync( "https://vpic.nhtsa.dot.gov/api/vehicles/GetMakesForVehicleType/car?format=json" );
                response.EnsureSuccessStatusCode();

                //IEnumerable<Brand> brands = await response.Content.ReadFromJsonAsync<IEnumerable<Brand>>();
                NhtsaResponseDTO responseDto = await response.Content.ReadFromJsonAsync<NhtsaResponseDTO>();
                if( responseDto?.Results != null ) {
                    // Map the DTO to your Brand class
                    var brands = responseDto.Results.Select( make => new Brand {
                        ID = make.MakeId, // Assuming Make_ID is the Brand ID
                        Name = make.MakeName,
                        CountryID = 0, // Set default or fetch from other data source
                        Country = null, // Set default or fetch from other data source
                        Cars = new List<Car>() // Initialize empty list or fetch from other data source
                    } ).ToList();

                    return brands;
                }
                return new List<Brand>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while getting all brands." );
                throw;
            }
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync() {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetAsync( "api/brands" );
                response.EnsureSuccessStatusCode();

                IEnumerable<Brand> brands = await response.Content.ReadFromJsonAsync<IEnumerable<Brand>>();
                return brands;
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while getting all brands." );
                throw;
            }
        }

        public async Task<Brand> GetBrandByIdAsync( int id ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetAsync( $"api/brands/{id}" );
                response.EnsureSuccessStatusCode();

                Brand brand = await response.Content.ReadFromJsonAsync<Brand>();
                return brand;
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while getting brand by id." );
                throw;
            }
        }

        public async Task<bool> CreateBrandAsync( Brand brand ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.PostAsJsonAsync( "api/brands/", brand );
                response.EnsureSuccessStatusCode();

                return response.IsSuccessStatusCode;
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while creating a brand." );
                throw;
            }
        }

        public async Task<Brand> UpdateBrandAsync( int id, Brand brand ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.PutAsJsonAsync( $"api/brands/{id}", brand );
                response.EnsureSuccessStatusCode();

                Brand result = await response.Content.ReadFromJsonAsync<Brand>();
                return result;
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while updating a brand." );
                throw;
            }
        }

        public async Task DeleteBrandAsync( int id ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.DeleteAsync( $"api/brands/{id}" );
                response.EnsureSuccessStatusCode();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while deleting a brand." );
                throw;
            }
        }

        #region Model

        public async Task<IEnumerable<Model.Model>> GetModelsForBrandApiAsync( int brandId ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetAsync( $"https://vpic.nhtsa.dot.gov/api/vehicles/GetModelsForMakeIdYear/makeId/{brandId}/vehicletype/car?format=json" );
                response.EnsureSuccessStatusCode();

                //IEnumerable<Brand> brands = await response.Content.ReadFromJsonAsync<IEnumerable<Brand>>();
                NhtsaResponseDTO responseDto = await response.Content.ReadFromJsonAsync<NhtsaResponseDTO>();
                if( responseDto?.Results != null ) {
                    // Map the DTO to your Brand class
                    var models = responseDto.Results.Select( make => new Model.Model {
                        Id = make.Model_ID ?? 0, // Assuming Make_ID is the Brand ID
                        Name = make.Model_Name,
                        BrandId = make.MakeId
                    } ).ToList();

                    return models;
                }
                return new List<Model.Model>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while getting models for brand." );
                throw;
            }
        }

        #endregion
    }
}
