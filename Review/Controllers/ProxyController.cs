using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Review.Controllers {
    [Route( "api/[controller]" )]
    [ApiController]
    public class ProxyController : ControllerBase {
        private readonly HttpClient _httpClient;

        public ProxyController( HttpClient httpClient ) {
            _httpClient = httpClient;
        }

        [HttpGet( "generate" )]
        public async Task<IActionResult> GenerateImage( string prompt ) {
            var response = await _httpClient.GetAsync( $"https://craiyon.ajaysinghusesgi.repl.co/api?prompt={prompt}" );
            if( response.IsSuccessStatusCode ) {
                var content = await response.Content.ReadAsStringAsync();
                return Content( content, "application/json" );
            }
            return StatusCode( (int) response.StatusCode, response.ReasonPhrase );
        }
    }
}
