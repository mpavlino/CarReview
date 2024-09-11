using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Review.Model.DTO;
using HtmlAgilityPack;
using Review.Model;

namespace Review.Test {
    public class Tests {
        private static readonly string baseUrl = "https://www.autoevolution.com/cars/";
        private static HttpClient httpClient = new HttpClient();

        [SetUp]
        public void Setup() {
            // Initialization code if needed
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() {
            httpClient.Dispose();
        }

        [Test]
        public async Task ScrapeCarMakesAndModels_ShouldReturnData() {
            var carData = await ScrapeCarData();

            // Example assertion: Check if car data is not empty
            Assert.IsNotEmpty( carData, "Car data should not be empty." );

            // Add more specific assertions based on expected results
            foreach( var engine in carData ) {
                //Assert.IsNotEmpty( make.Value, $"Model list for {make.Key} should not be empty." );
                Assert.IsNotEmpty( engine.Name, $"Engine list for {engine.CarID} should not be empty." );
            }
        }

        private static async Task<string> GetContentWithRetry( string url, int maxRetries = 5 ) {
            int delay = 1000; // Start with a 1-second delay
            for( int attempt = 1; attempt <= maxRetries; attempt++ ) {
                try {
                    var response = await httpClient.GetAsync( url );
                    if( response.IsSuccessStatusCode ) {
                        return await response.Content.ReadAsStringAsync();
                    }

                    if( response.StatusCode == System.Net.HttpStatusCode.TooManyRequests ) {
                        // Check if the response has a Retry-After header
                        if( response.Headers.TryGetValues( "Retry-After", out var values ) ) {
                            var retryAfter = values.FirstOrDefault();
                            if( int.TryParse( retryAfter, out var retrySeconds ) ) {
                                delay = retrySeconds * 1000; // Convert to milliseconds
                            }
                        }
                    }
                    response.EnsureSuccessStatusCode(); // Throw if not a success status code
                }
                catch( HttpRequestException ex ) when( ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests ) {
                    // Log or handle the specific rate limit exception if necessary
                }
                // Wait before retrying
                await Task.Delay( delay );
                delay *= 2; // Exponential backoff
            }
            throw new HttpRequestException( $"Failed to retrieve content from {url} after {maxRetries} attempts." );
        }


        private static async Task<List<EngineDTO>> ScrapeCarData() {
            var carData = new Dictionary<string, List<string>>();
            var engines = new List<EngineDTO>();

            // Fetch the main cars page
            var mainPageContent = await GetContentWithRetry( baseUrl );
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml( mainPageContent );

            // Print the HTML content for debugging
            Console.WriteLine( htmlDocument.DocumentNode.OuterHtml );

            // Simplified XPath to verify if we can get any div elements
            var allDivs = htmlDocument.DocumentNode.SelectNodes( "//div" );
            if( allDivs != null ) {
                foreach( var div in allDivs ) {
                    Console.WriteLine( div.OuterHtml ); // Print out each div for inspection
                }
            }

            // Try simplified XPath to see if we can locate any 'a' tags
            var links = htmlDocument.DocumentNode.SelectNodes( "//a" );
            if( links != null ) {
                foreach( var link in links ) {
                    Console.WriteLine( link.OuterHtml ); // Print out each link for inspection
                }
            }

            // Updated XPath to find divs with class 'carman' and then all 'a' tags within them
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
                    var makePageContent = await GetContentWithRetry( makeUrl );
                    var makeHtmlDocument = new HtmlDocument();
                    makeHtmlDocument.LoadHtml( makePageContent );

                    // XPath to find all model names inside <h4> tags within <div> with class 'carmod'
                    var modelNodes = makeHtmlDocument.DocumentNode.SelectNodes( "//div[contains(@class, 'carmod')]//h4" );
                    var modelNames = new List<string>();

                    if( modelNodes != null ) {
                        foreach( var modelNode in modelNodes ) {
                            modelNames.Add( modelNode.InnerText.Trim() );

                        }
                    }

                    carData[makeName] = modelNames;

                    var modelUrlNodes = makeHtmlDocument.DocumentNode.SelectNodes( "//div[contains(@class, 'carmod')]//a[1]" );

                    foreach( var modelUrlNode in modelUrlNodes ) {
                        var modelUrl = modelUrlNode.GetAttributeValue( "href", string.Empty );

                        // Ensure the URL is absolute
                        if( !modelUrl.StartsWith( "http" ) ) {
                            modelUrl = new Uri( new Uri( baseUrl ), modelUrl ).AbsoluteUri;
                        }

                        // Fetch the make's page to get models
                        var modelPageContent = await GetContentWithRetry( modelUrl );
                        var modelHtmlDocument = new HtmlDocument();
                        modelHtmlDocument.LoadHtml( modelPageContent );

                        // XPath to find all model names inside <h4> tags within <div> with class 'carmod'
                        var modelDataNodes = modelHtmlDocument.DocumentNode.SelectNodes( "//div[contains(@class, 'carmodel')]//span[contains(@class, 'col-red')]" );
                        var modelGenerationNames = new List<string>();

                        if( modelDataNodes != null ) {
                            foreach( var modelData in modelDataNodes ) {
                                modelGenerationNames.Add( modelData.InnerText.Trim() );
                            }
                        }

                        var generationUrlNodes = modelHtmlDocument.DocumentNode.SelectNodes( "//div[contains(@class, 'carmodel')]//a[1]" );
                        foreach( var generationUrlNode in generationUrlNodes ) {
                            var generationUrl = generationUrlNode.GetAttributeValue( "href", string.Empty );

                            // Ensure the URL is absolute
                            if( !generationUrl.StartsWith( "http" ) ) {
                                generationUrl = new Uri( new Uri( baseUrl ), generationUrl ).AbsoluteUri;
                            }

                            // Fetch the generation page to get models
                            var generationPageContent = await GetContentWithRetry( generationUrl );
                            var generationHtmlDocument = new HtmlDocument();
                            generationHtmlDocument.LoadHtml( generationPageContent );

                            // XPath to find all model names inside <h4> tags within <div> with class 'carmod'
                            var generationDataNodes = generationHtmlDocument.DocumentNode.SelectSingleNode( "//div[contains(@class, 'modelbox')]//p" );
                            var modelGenerationData = generationDataNodes.InnerText.Trim();

                            var yearNodes = generationHtmlDocument.DocumentNode.SelectSingleNode( "//span[contains(@class, 'motlisthead_years')]" );
                            string generationYears = yearNodes.InnerText.Trim();
                            var engineTypeNodes = generationHtmlDocument.DocumentNode.SelectNodes( "//div[contains(@class, 'sbox10')]//div[contains(@class, 'tt')]" );
                            var engineTypes = new List<string>();
                            foreach( var engineTypeNode in engineTypeNodes ) {
                                engineTypes.Add( engineTypeNode.InnerText.Trim() );
                            }

                            var engineNodes = generationHtmlDocument.DocumentNode.SelectNodes( "//div[contains(@class, 'sbox10')]//ul" );
                            var engineList = new List<string>();
                            foreach( var engine in engineNodes ) {
                                var engineListNodes = engine.SelectNodes( ".//li[contains(@class, 'ellip')]" );
                                foreach( var engineNode in engineListNodes ) {
                                    engineList.Add( engineNode.InnerText.Trim() );
                                    var engineFragment = engineNode.GetAttributeValue( "id", string.Empty );

                                    // Construct the correct engine URL format
                                    if( !string.IsNullOrEmpty( engineFragment ) ) {
                                        var baseGenerationUrl = generationUrl.Split( '#' )[0]; // Remove any existing fragment
                                        var formattedEngineUrl = $"{baseGenerationUrl}#aeng_{engineFragment}";

                                        // Fetch the engine page to get engine data
                                        var enginePageContent = await GetContentWithRetry( formattedEngineUrl );
                                        var engineHtmlDocument = new HtmlDocument();
                                        engineHtmlDocument.LoadHtml( enginePageContent );
                                        var engineDataNodes = engineHtmlDocument.DocumentNode.SelectNodes( "//div[contains(@class, 'enginedata')]//table[contains(@class, 'techdata')]" );
                                        foreach( var engineDataNode in engineDataNodes ) {
                                            var textDiv = engineDataNode.SelectSingleNode( ".//th//div" );
                                            var text = textDiv?.InnerText;
                                            // Further processing of the engine data
                                        }
                                        // Assuming you have a method to scrape engine data
                                        engines.AddRange(await ScrapeEngineData( formattedEngineUrl ));
                                    }
                                }
                            }
                        }
                        // Optional: Add a delay to prevent overloading the server
                        await Task.Delay( 1000 ); // Adjust the delay as necessary
                    }
                }
            }
            return engines;
        }

        private static async Task<List<EngineDTO>> ScrapeEngineData( string engineUrl ) {
            var engines = new List<EngineDTO>();

            // Fetch the engine details page
            var enginePageContent = await GetContentWithRetry( engineUrl );
            var engineHtmlDocument = new HtmlDocument();
            engineHtmlDocument.LoadHtml( enginePageContent );

            // Extracting engine data from tables with class 'techdata'
            var engineDataNodes = engineHtmlDocument.DocumentNode.SelectNodes( "//table[@class='techdata']" );
            var engineDTO = new EngineDTO();
            if( engineDataNodes != null ) {
                foreach( var engineDataNode in engineDataNodes ) {
                    // Extract engine specifications
                    var engineSpecsTitleNode = engineDataNode.SelectSingleNode( ".//th[@class='title']/div" );
                    if( engineSpecsTitleNode != null ) {
                        // Extract engine title e.g. "35 TFSI 6MT (150 HP)"
                        var engineName = engineSpecsTitleNode.SelectSingleNode( ".//span[@class='col-green']" );
                        if( engineName != null ) {
                            engineDTO.Name = engineName.InnerText;
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

                            // Map specs to EngineDTO properties based on the title
                            switch( specTitle ) {
                                case "Cylinders:":
                                    engineDTO.Cylinders = specValue;
                                    break;
                                case "Displacement:":
                                    engineDTO.Displacement = specValue;
                                    break;
                                case "Power:":
                                    engineDTO.Power = specValue;
                                    break;
                                case "Torque:":
                                    engineDTO.Torque = specValue;
                                    break;
                                case "Fuel System:":
                                    engineDTO.FuelSystem = specValue;
                                    break;
                                case "Fuel:":
                                    engineDTO.FuelType = specValue;
                                    break;
                                case "Fuel capacity:":
                                    engineDTO.FuelCapacity = decimal.TryParse( specValue.Split( ' ' )[0], out var capacity ) ? capacity : (decimal?) null;
                                    break;
                                case "Top Speed:":
                                    engineDTO.TopSpeed = int.TryParse( specValue.Split( ' ' )[0], out var topSpeed ) ? topSpeed : (int?) null;
                                    break;
                                case "Acceleration 0-62 Mph (0-100 kph):":
                                    engineDTO.Acceleration = decimal.TryParse( specValue.Split( ' ' )[0], out var acceleration ) ? acceleration : (decimal?) null;
                                    break;
                                case "Drive Type:":
                                    engineDTO.DriveType = specValue;
                                    break;
                                case "Gearbox:":
                                    engineDTO.Gearbox = specValue;
                                    break;
                                case "Front Brakes:":
                                    engineDTO.FrontBrakes = specValue;
                                    break;
                                case "Rear Brakes:":
                                    engineDTO.RearBrakes = specValue;
                                    break;
                                case "Tire Size:":
                                    engineDTO.TireSize = specValue;
                                    break;
                                case "Length:":
                                    engineDTO.Length = specValue;
                                    break;
                                case "Width:":
                                    engineDTO.Width = specValue;
                                    break;
                                case "Height:":
                                    engineDTO.Height = specValue;
                                    break;
                                case "Front/Rear Track:":
                                    engineDTO.FrontRearTrack = specValue;
                                    break;
                                case "Wheelbase:":
                                    engineDTO.Wheelbase = specValue;
                                    break;
                                case "Ground Clearance:":
                                    engineDTO.GroundClearance = specValue;
                                    break;
                                case "Cargo Volume:":
                                    engineDTO.CargoVolume = specValue;
                                    break;
                                case "Unladen Weight:":
                                    engineDTO.UnladenWeight = specValue;
                                    break;
                                case "Gross Weight Limit:":
                                    engineDTO.GrossWeightLimit = specValue;
                                    break;
                                case "Fuel Economy (City):":
                                    engineDTO.FuelEconomyCity = specValue;
                                    break;
                                case "Fuel Economy (Highway):":
                                    engineDTO.FuelEconomyHighway = specValue;
                                    break;
                                case "Fuel Economy (Combined):":
                                    engineDTO.FuelEconomyCombined = specValue;
                                    break;
                                case "CO2 Emissions:":
                                    engineDTO.CO2Emissions = specValue;
                                    break;
                                default:
                                    // Handle any additional specs if necessary
                                    break;
                            }
                        }
                    }
                }
                // Add the constructed EngineDTO to the list
                engines.Add( engineDTO );
            }
            return engines;
        }
    }
}
