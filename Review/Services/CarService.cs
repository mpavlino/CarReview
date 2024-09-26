using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Review.DAL;
using Review.Model.DTO;
using Review.Model;
using Review.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Review.Models;
using System.Net.Http.Headers;
using Review.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TextTemplating;

namespace Review.Services {
    public class CarService : BaseService, ICarService {

        private CarManagerDbContext _dbContext;
        private readonly ILogger<CarService> _logger;
        private readonly IBrandService _brandService;

        public CarService( CarManagerDbContext dbContext, HttpClient httpClient, TokenHandler tokenHandler, ILogger<CarService> logger,
            UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IBrandService brandService )
            : base( httpClient, userManager, httpContextAccessor, tokenHandler ) {
            _dbContext = dbContext ?? throw new ArgumentNullException( nameof( dbContext ) );
            _logger = logger ?? throw new ArgumentNullException( nameof( logger ) );
            _brandService = brandService;
        }

        //public bool IsCarModelNameUnique( string name ) {
        //    return !_dbContext.Cars.Any( b => b.Model == name );
        //}

        public bool IsCarGenerationNameUnique( string name ) {
            return !_dbContext.Cars.Any( b => b.Generation == name );
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync() {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetAsync( "api/cars" );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Car>>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while getting all cars." );
                throw;
            }
        }

        public async Task<Car> GetCarByIdAsync( int id ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetAsync( $"api/cars/{id}" );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Car>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while getting car with ID {id}." );
                throw;
            }
        }

        public async Task<IEnumerable<Car>> SearchCarsAsync( CarFilterModel filter ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.PostAsJsonAsync( $"api/cars/search", filter );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Car>>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while searching cars with query." );
                throw;
            }
        }

        public async Task<IEnumerable<Car>> SearchCarsByTextAsync( string query ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.PostAsJsonAsync( $"api/cars/query", query );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Car>>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while searching cars with text query." );
                throw;
            }
        }

        public async Task<bool> CreateCarAsync( Car car ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.PostAsJsonAsync( "api/cars", car );
                response.EnsureSuccessStatusCode();

                return response.IsSuccessStatusCode;
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while creating a car." );
                throw;
            }
        }

        public async Task<Car> UpdateCarAsync( int id, Car model ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.PutAsJsonAsync( $"api/cars/{id}", model );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Car>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while updating car with ID {id}." );
                throw;
            }
        }

        public async Task DeleteCarAsync( int id ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.DeleteAsync( $"api/cars/{id}" );
                response.EnsureSuccessStatusCode();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while deleting car with ID {id}." );
                throw;
            }
        }


        public async Task<bool> SyncCarsAsync( int id ) {
            try {
                var cars = await GetAllCarsFromWebAsync( id );
                var allCars = await GetAllCarsAsync();
                foreach( var car in cars ) {
                    var carExists = allCars.Where( x => x.ModelID == car.ModelID && x.Generation == car.Generation );
                    if( !carExists.Any() ) {
                        await CreateCarAsync( car );
                    }
                }
                return true;
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while syncing cars. {ex.Message}" );
                throw;
            }
        }


        public async Task<IEnumerable<Car>> GetAllCarsFromWebAsync( int id ) {
            try {
                await SetAuthorizationHeaderAsync();
                var cars = new List<Car>();
                string baseUrl = "https://www.autoevolution.com/cars/";

                var model = await _brandService.GetModelById( id );
                //var brandName = model.Brand?.Name.Replace( " ", "-" );
                //var modelName = model.Name.Replace( " ", "-" );
                //var modelUrl = $"https://www.autoevolution.com/{brandName.ToLower()}/{modelName.ToLower()}";
                var modelUrl = model.Url;
                // Fetch the make's page to get models
                var modelPageContent = await _httpClient.GetStringAsync( modelUrl );
                var modelHtmlDocument = new HtmlDocument();
                modelHtmlDocument.LoadHtml( modelPageContent );

                var generationUrlNodes = modelHtmlDocument.DocumentNode.SelectNodes( "//div[contains(@class, 'carmodel')]//h2//a[1]" );
                foreach( var generationUrlNode in generationUrlNodes ) {
                    var car = new Car();
                    car.BrandID = model.BrandId;
                    car.ModelID = model.Id;
                    car.Engines = new List<Model.Engine>();
                    var generationUrl = generationUrlNode.GetAttributeValue( "href", string.Empty );

                    if( generationUrl != null && generationUrl.Contains( baseUrl ) ) {
                        // Ensure the URL is absolute
                        if( !generationUrl.StartsWith( "http" ) ) {
                            generationUrl = new Uri( new Uri( baseUrl ), generationUrl ).AbsoluteUri;
                        }

                        // Fetch the generation page to get models
                        var generationPageContent = await _httpClient.GetStringAsync( generationUrl );
                        var generationHtmlDocument = new HtmlDocument();
                        generationHtmlDocument.LoadHtml( generationPageContent );

                        var generationNameNode = generationHtmlDocument.DocumentNode.SelectSingleNode( "//h1/a[1]" );
                        if( generationNameNode != null ) {
                            car.Generation = generationNameNode.InnerText.Replace( "Photos, engines &amp; full specs", "" ).Trim();
                        }

                        var generationImageNode = generationHtmlDocument.DocumentNode.SelectSingleNode( "//img[@class='curpo']" );
                        var imageUrl = generationImageNode.GetAttributeValue( "src", string.Empty );
                        byte[] imageBytes = await _httpClient.GetByteArrayAsync( imageUrl );
                        car.ImageData = imageBytes;

                        // XPath to find all model names inside <h4> tags within <div> with class 'carmod'
                        var generationDataNodes = generationHtmlDocument.DocumentNode.SelectSingleNode( "//div[contains(@class, 'modelbox')]//p" );
                        var modelGenerationData = generationDataNodes.InnerText.Trim(); //description

                        var yearNodes = generationHtmlDocument.DocumentNode.SelectSingleNode( "//span[contains(@class, 'motlisthead_years')]" );
                        var generationYears = yearNodes.InnerText.Split( ',' );
                        car.ModelYearFrom = new DateTime( Convert.ToInt32( generationYears.First() ), 1, 1 );
                        if( generationYears.Length > 1 ) {
                            car.ModelYearTo = new DateTime( Convert.ToInt32( generationYears.Last() ), 1, 1 );
                        }
                        var engineNodes = generationHtmlDocument.DocumentNode.SelectNodes( "//div[contains(@class, 'sbox10')]//ul" );
                        var engineList = new List<string>();
                        if( engineNodes != null ) {
                            foreach( var engine in engineNodes ) {
                                var carEngine = new Model.Engine();
                                var engineListNodes = engine.SelectNodes( ".//li[contains(@class, 'ellip')]" );
                                if( engineListNodes != null ) {
                                    foreach( var engineNode in engineListNodes ) {
                                        engineList.Add( engineNode.InnerText.Trim() );
                                        var engineFragment = engineNode.GetAttributeValue( "id", string.Empty );

                                        // Construct the correct engine URL format
                                        if( !string.IsNullOrEmpty( engineFragment ) ) {
                                            var baseGenerationUrl = generationUrl.Split( '#' )[0]; // Remove any existing fragment
                                            var formattedEngineUrl = $"{baseGenerationUrl}#aeng_{engineFragment}";

                                            // Fetch the engine page to get engine data
                                            var enginePageContent = await _httpClient.GetStringAsync( formattedEngineUrl );
                                            var engineHtmlDocument = new HtmlDocument();
                                            engineHtmlDocument.LoadHtml( enginePageContent );

                                            // Assuming you have a method to scrape engine data
                                            var engines = new List<Model.Engine>();
                                            engines = await ScrapeEngineData( formattedEngineUrl );
                                            car.Engines.AddRange( engines );
                                        }
                                        await Task.Delay( 500 );
                                    }
                                }
                                await Task.Delay( 500 );
                            }
                        }
                        await Task.Delay( 500 );
                    }
                    cars.Add( car );
                }
                //await Task.Delay( 500 );
                //await CreateCarAsync( car );                                                                     
                return cars;
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while syncing cars." );
                throw;
            }
        }

        private async Task<List<Model.Engine>> ScrapeEngineData( string engineUrl ) {
            var engines = new List<Model.Engine>();

            // Fetch the engine details page
            var enginePageContent = await _httpClient.GetStringAsync( engineUrl );
            var engineHtmlDocument = new HtmlDocument();
            engineHtmlDocument.LoadHtml( enginePageContent );

            var engineId = engineUrl.Split( '#' )[1];
            engineId = engineId.Replace( "aeng_li_", "" );
            // Extracting engine data from tables with class 'techdata'
            var engineDataNodes = engineHtmlDocument.DocumentNode.SelectNodes( $"//div[@id='{engineId}']//table[@class='techdata']" );
            var engine = new Model.Engine();
            if( engineDataNodes != null ) {
                foreach( var engineDataNode in engineDataNodes ) {
                    // Extract engine specifications
                    var engineSpecsTitleNode = engineDataNode.SelectSingleNode( ".//th[@class='title']/div" );
                    if( engineSpecsTitleNode != null ) {
                        // Extract engine title e.g. "35 TFSI 6MT (150 HP)"
                        var engineName = engineSpecsTitleNode.SelectSingleNode( ".//span[@class='col-green']" );
                        if( engineName != null ) {
                            engine.Name = engineName.InnerText;
                        }
                    }

                    // Extract individual specs based on the table rows
                    var specRows = engineDataNode.SelectNodes( ".//tr" );

                    foreach( var row in specRows ) {
                        var leftColumn = row.SelectSingleNode( ".//td[@class='left']/strong" );
                        var rightColumn = row.SelectSingleNode( ".//td[@class='right']" );

                        if( leftColumn != null && rightColumn != null ) {
                            var specTitle = leftColumn.InnerText.Trim();
                            var specValue = rightColumn.InnerText.Trim();

                            // Map specs to Model.Engine properties based on the title
                            switch( specTitle ) {
                                case "Cylinders:":
                                    engine.Cylinders = specValue;
                                    break;
                                case "Displacement:":
                                    engine.Displacement = specValue;
                                    break;
                                case "Power:":
                                    engine.Power = specValue;
                                    break;
                                case "Torque:":
                                    engine.Torque = specValue;
                                    break;
                                case "Fuel System:":
                                    engine.FuelSystem = specValue;
                                    break;
                                case "Fuel:":
                                    engine.FuelType = specValue;
                                    break;
                                case "Fuel capacity:":
                                    engine.FuelCapacity = decimal.TryParse( specValue.Split( ' ' )[0], out var capacity ) ? capacity : (decimal?) null;
                                    break;
                                case "Top Speed:":
                                    engine.TopSpeed = int.TryParse( specValue.Split( ' ' )[0], out var topSpeed ) ? topSpeed : (int?) null;
                                    break;
                                case "Acceleration 0-62 Mph (0-100 kph):":
                                    engine.Acceleration = decimal.TryParse( specValue.Split( ' ' )[0], out var acceleration ) ? acceleration : (decimal?) null;
                                    break;
                                case "Drive Type:":
                                    engine.DriveType = specValue;
                                    break;
                                case "Gearbox:":
                                    engine.Gearbox = specValue;
                                    break;
                                case "Front Brakes:":
                                    engine.FrontBrakes = specValue;
                                    break;
                                case "Rear Brakes:":
                                    engine.RearBrakes = specValue;
                                    break;
                                case "Tire Size:":
                                    engine.TireSize = specValue;
                                    break;
                                case "Length:":
                                    engine.Length = specValue;
                                    break;
                                case "Width:":
                                    engine.Width = specValue;
                                    break;
                                case "Height:":
                                    engine.Height = specValue;
                                    break;
                                case "Front/Rear Track:":
                                    engine.FrontRearTrack = specValue;
                                    break;
                                case "Wheelbase:":
                                    engine.Wheelbase = specValue;
                                    break;
                                case "Ground Clearance:":
                                    engine.GroundClearance = specValue;
                                    break;
                                case "Cargo Volume:":
                                    engine.CargoVolume = specValue;
                                    break;
                                case "Unladen Weight:":
                                    engine.UnladenWeight = specValue;
                                    break;
                                case "Gross Weight Limit:":
                                    engine.GrossWeightLimit = specValue;
                                    break;
                                case "Fuel Economy (City):":
                                    engine.FuelEconomyCity = specValue;
                                    break;
                                case "Fuel Economy (Highway):":
                                    engine.FuelEconomyHighway = specValue;
                                    break;
                                case "Fuel Economy (Combined):":
                                    engine.FuelEconomyCombined = specValue;
                                    break;
                                case "CO2 Emissions:":
                                    engine.CO2Emissions = specValue;
                                    break;
                                default:
                                    // Handle any additional specs if necessary
                                    break;
                            }
                        }
                    }
                }
                // Add the constructed Model.Engine to the list
                engines.Add( engine );
            }
            return engines;
        }


        #region CarReview

        public async Task<List<Model.CarReview>> GetCarReviewsByCarIdAsync( int id ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetAsync( $"api/cars/reviews/{id}" );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<List<Model.CarReview>>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while getting reviews with car ID {id}." );
                throw;
            }
        }

        public async Task<CarReview> GetCarReviewByIdAsync( int id ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetAsync( $"api/cars/review/{id}" );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<CarReview>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while getting review with ID {id}." );
                throw;
            }
        }

        public async Task<bool> CreateCarReviewAsync( CarReview carReview ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.PostAsJsonAsync( "api/cars/review", carReview );
                response.EnsureSuccessStatusCode();

                return response.IsSuccessStatusCode;
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while creating a car review." );
                throw;
            }
        }

        public async Task<CarReview> UpdateCarReviewAsync( int id, CarReview model ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.PutAsJsonAsync( $"api/cars/review/{id}", model );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<CarReview>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while updating car review with ID {id}." );
                throw;
            }
        }

        public async Task DeleteCarReviewAsync( int id ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.DeleteAsync( $"api/cars/review/{id}" );
                response.EnsureSuccessStatusCode();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while deleting car review with ID {id}." );
                throw;
            }
        }

        #endregion
    }
}
