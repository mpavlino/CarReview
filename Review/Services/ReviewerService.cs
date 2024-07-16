using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Review.DAL;
using Review.Handlers;
using Review.Model;
using Review.Model.DTO;
using Review.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Review.Services {
    public class ReviewerService : BaseService, IReviewerService {

        private readonly CarManagerDbContext _dbContext;
        private readonly ILogger<ReviewerService> _logger;

        public ReviewerService( CarManagerDbContext dbContext, HttpClient httpClient, TokenHandler tokenHandler, ILogger<ReviewerService> logger, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor )
            : base( httpClient, userManager, httpContextAccessor, tokenHandler ) {
            _dbContext = dbContext ?? throw new ArgumentNullException( nameof( dbContext ) );
            _logger = logger ?? throw new ArgumentNullException( nameof( logger ) );
        }

        public async Task<IEnumerable<Reviewer>> GetAllReviewersAsync() {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetAsync( "api/reviewers" );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Reviewer>>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while getting all reviewers." );
                throw;
            }
        }

        public async Task<Reviewer> GetReviewerByIdAsync( int id ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.GetAsync( $"api/reviewers/{id}" );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Reviewer>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while getting reviewer with ID {id}." );
                throw;
            }
        }

        public async Task<bool> CreateReviewerAsync( Reviewer reviewer ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.PostAsJsonAsync( "api/reviewers", reviewer );
                response.EnsureSuccessStatusCode();

                return response.IsSuccessStatusCode;
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while creating a reviewer." );
                throw;
            }
        }

        public async Task<Reviewer> UpdateReviewerAsync( int id, Reviewer model ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.PutAsJsonAsync( $"api/reviewers/{id}", model );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Reviewer>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while updating reviewer with ID {id}." );
                throw;
            }
        }

        public async Task DeleteReviewerAsync( int id ) {
            try {
                await SetAuthorizationHeaderAsync();
                var response = await _httpClient.DeleteAsync( $"api/reviewers/{id}" );
                response.EnsureSuccessStatusCode();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while deleting reviewer with ID {id}." );
                throw;
            }
        }
    }
}
