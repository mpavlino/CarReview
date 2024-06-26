using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Review.DAL;
using Review.Model;
using Review.Model.DTO;
using Review.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Review.Services {
    public class ReviewerService : IReviewerService {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ReviewerService> _logger;

        public ReviewerService( HttpClient httpClient, ILogger<ReviewerService> logger ) {
            _httpClient = httpClient ?? throw new ArgumentNullException( nameof( httpClient ) );
            _logger = logger ?? throw new ArgumentNullException( nameof( logger ) );
        }

        public async Task<IEnumerable<ReviewerDTO>> GetAllReviewersAsync() {
            try {
                var response = await _httpClient.GetAsync( "api/reviewers" );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<ReviewerDTO>>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while getting all reviewers." );
                throw;
            }
        }

        public async Task<ReviewerDTO> GetReviewerByIdAsync( int id ) {
            try {
                var response = await _httpClient.GetAsync( $"api/reviewers/{id}" );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<ReviewerDTO>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while getting reviewer with ID {id}." );
                throw;
            }
        }

        public async Task<ReviewerDTO> CreateReviewerAsync( Reviewer reviewer ) {
            try {
                var response = await _httpClient.PostAsJsonAsync( "api/reviewers", reviewer );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<ReviewerDTO>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while creating a reviewer." );
                throw;
            }
        }

        public async Task<ReviewerDTO> UpdateReviewerAsync( int id, Reviewer reviewer ) {
            try {
                var response = await _httpClient.PutAsJsonAsync( $"api/reviewers/{id}", reviewer );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<ReviewerDTO>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while updating reviewer with ID {id}." );
                throw;
            }
        }

        public async Task DeleteReviewerAsync( int id ) {
            try {
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
