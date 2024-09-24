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
using HtmlAgilityPack;

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
                //IEnumerable<Brand> externalBrands = await GetAllBrandsFromApiAsync(); // This calls the external API
                IEnumerable<Brand> externalBrands = await GetAllBrandsFromWebAsync(); // This calls the external API

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
                if( response.IsSuccessStatusCode ) {
                    IEnumerable<Model.Model> externalModels = await GetAllModelsFromWebAsync(); // This calls the external API
                                                                                                // Prepare the data for the sync API
                    var syncModelData = externalModels.Select( model => new Model.Model {
                        Id = model.Id,
                        Name = model.Name,
                        BrandId = model.BrandId
                    } ).ToList();

                    // Call your API controller's sync endpoint
                    await SetAuthorizationHeaderAsync();
                    var responseModel = await _httpClient.PostAsJsonAsync( "api/brands/models/sync", syncModelData );
                    responseModel.EnsureSuccessStatusCode();

                    return responseModel.IsSuccessStatusCode;
                }

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

        public async Task<IEnumerable<Brand>> GetAllBrandsFromWebAsync() {
            try {
                await SetAuthorizationHeaderAsync();
                var brands = new List<Brand>();
                string baseUrl = "https://www.autoevolution.com/cars/";

                // Fetch the main cars page
                var mainPageContent = await _httpClient.GetStringAsync( baseUrl );
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml( mainPageContent );

                // Print the HTML content for debugging
                // Console.WriteLine(htmlDocument.DocumentNode.OuterHtml);

                // Updated XPath to find divs with class 'carman' and then all 'a' tags within them
                var makeNodes = htmlDocument.DocumentNode.SelectNodes( "//div[contains(@class, 'carman')]/a[1]" );

                if( makeNodes != null ) {
                    foreach( var makeNode in makeNodes ) {
                        // Extract the car make's name from the title attribute
                        var makeName = makeNode.GetAttributeValue( "title", string.Empty ).Trim();

                        // Create a new Brand instance and add it to the list
                        var brand = new Brand {
                            Name = makeName,
                            // Assuming CountryID can be null or set to a default value
                            CountryID = null
                        };
                        brands.Add( brand );
                    }
                }

                return brands;
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while getting all brands." );
                throw;
            }
        }

        // Helper method to get BrandId by makeName
        private async Task<int> GetBrandIdByNameAsync( string makeName ) {
            try {
                var response = await _httpClient.GetAsync( $"api/brands/{Uri.EscapeDataString( makeName )}" );
                response.EnsureSuccessStatusCode();

                var brand = await response.Content.ReadFromJsonAsync<Brand>();
                return brand?.ID ?? 0;
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while getting brand ID by name." );
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

        public async Task<IEnumerable<Model.Model>> GetAllModelsFromWebAsync() {
            try {
                await SetAuthorizationHeaderAsync();
                var models = new List<Model.Model>();
                string baseUrl = "https://www.autoevolution.com/cars/";

                // Fetch the main cars page
                var mainPageContent = await _httpClient.GetStringAsync( baseUrl );
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml( mainPageContent );

                // XPath to find divs with class 'carman' and then all 'a' tags within them
                var makeNodes = htmlDocument.DocumentNode.SelectNodes( "//div[contains(@class, 'carman')]/a[1]" );

                if( makeNodes != null ) {
                    foreach( var makeNode in makeNodes ) {
                        // Extract the car make's name from the title attribute
                        var makeName = makeNode.GetAttributeValue( "title", string.Empty ).Trim();
                        var makeUrl = makeNode.GetAttributeValue( "href", string.Empty );

                        // Ensure the URL is absolute
                        if( !makeUrl.StartsWith( "http" ) ) {
                            makeUrl = new Uri( new Uri( baseUrl ), makeUrl ).AbsoluteUri;
                        }

                        // Fetch the make's page to get models
                        var makePageContent = await _httpClient.GetStringAsync( makeUrl );
                        var makeHtmlDocument = new HtmlDocument();
                        makeHtmlDocument.LoadHtml( makePageContent );

                        // XPath to find all model names inside <h4> tags within <div> with class 'carmod'
                        var modelNodes = makeHtmlDocument.DocumentNode.SelectNodes( "//div[contains(@class, 'carmod')]//h4" );

                        if( modelNodes != null ) {
                            foreach( var modelNode in modelNodes ) {
                                var modelName = modelNode.InnerText.Trim();
                                modelName = modelName.Substring( makeName.Length ).Trim();

                                // Create a new Model instance and add it to the list
                                var model = new Model.Model {
                                    Name = modelName,
                                    BrandId = await GetBrandIdByNameAsync( makeName ) // Method to get BrandId by makeName
                                };
                                models.Add( model );
                            }
                        }

                        // Optional: Add a delay to prevent overloading the server
                        await Task.Delay( 300 ); // Adjust the delay as necessary
                    }
                }

                return models;
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while getting all models." );
                throw;
            }
        }

        public async Task<IEnumerable<Model.Model>> GetModelsByBrandId( int brandId ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetAsync( $"api/brands/models/{brandId}" );
                response.EnsureSuccessStatusCode();

                IEnumerable<Model.Model> models = await response.Content.ReadFromJsonAsync<IEnumerable<Model.Model>>();
                return models;
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while getting models for brand ID {brandId}. {ex.Message}" );
                throw;
            }
        }

        public async Task<Model.Model> GetModelById( int id ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetAsync( $"api/brands/model/{id}" );
                response.EnsureSuccessStatusCode();

                Model.Model model = await response.Content.ReadFromJsonAsync<Model.Model>();
                return model;
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while getting model by ID {id}. {ex.Message}" );
                throw;
            }
        }

        #endregion
    }
}
