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

namespace Review.Services {
    public class BrandService : IBrandService {

        private readonly CarManagerDbContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly ILogger<BrandService> _logger;

        public BrandService( CarManagerDbContext dbContext, HttpClient httpClient, ILogger<BrandService> logger ) {
            _dbContext = dbContext;
            _httpClient = httpClient;
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

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync() {
            try {
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

        public async Task<Brand> CreateBrandAsync( Brand brand ) {
            try {
                var response = await _httpClient.PostAsJsonAsync( "api/brands/", brand );
                response.EnsureSuccessStatusCode();

                Brand result = await response.Content.ReadFromJsonAsync<Brand>();
                return result;
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while creating a brand." );
                throw;
            }
        }

        public async Task<Brand> UpdateBrandAsync( int id, JObject brand ) {
            try {
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
                var response = await _httpClient.DeleteAsync( $"api/brands/{id}" );
                response.EnsureSuccessStatusCode();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while deleting a brand." );
                throw;
            }
        }
    }
}
