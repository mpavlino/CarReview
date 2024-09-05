using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

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
            foreach( var make in carData ) {
                Assert.IsNotEmpty( make.Value, $"Model list for {make.Key} should not be empty." );
            }
        }

        private static async Task<Dictionary<string, List<string>>> ScrapeCarData() {
            var carData = new Dictionary<string, List<string>>();

            // Fetch the main cars page
            var mainPageContent = await httpClient.GetStringAsync( baseUrl );
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
            var makeNodes = htmlDocument.DocumentNode.SelectNodes( "//div[contains(@class, 'carman')]//a" );

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
                    var makePageContent = await httpClient.GetStringAsync( makeUrl );
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

                    // Optional: Add a delay to prevent overloading the server
                    await Task.Delay( 300 ); // Adjust the delay as necessary
                }
            }

            return carData;
        }
    }
}
