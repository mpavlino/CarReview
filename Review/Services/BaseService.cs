using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Review.Handlers;
using Review.Model;

namespace Review.Services {
    public abstract class BaseService {
        protected readonly HttpClient _httpClient;
        protected readonly UserManager<AppUser> _userManager;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly TokenHandler _tokenHandler;

        public BaseService( HttpClient httpClient, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, TokenHandler tokenHandler ) {
            _httpClient = httpClient ?? throw new ArgumentNullException( nameof( httpClient ) );
            _userManager = userManager ?? throw new ArgumentNullException( nameof( userManager ) );
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException( nameof( httpContextAccessor ) );
            _tokenHandler = tokenHandler ?? throw new ArgumentNullException( nameof( tokenHandler ) );
        }

        protected async Task SetAuthorizationHeaderAsync() {
            var user = _httpContextAccessor.HttpContext.User;
            var userId = await _userManager.GetUserIdAsync( await _userManager.GetUserAsync( user ) );

            if( string.IsNullOrEmpty( userId ) ) {
                throw new UnauthorizedAccessException( "User is not authenticated" );
            }

            string token = _tokenHandler.GenerateToken( userId );
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", token );

            // Add User-Agent header
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd( "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36" );
        }
    }
}
