﻿using Microsoft.CodeAnalysis.Elfie.Diagnostics;
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

namespace Review.Services {
    public class CarService : ICarService {

        private CarManagerDbContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly ILogger<CarService> _logger;

        public CarService( CarManagerDbContext dbContext, HttpClient httpClient, ILogger<CarService> logger ) {
            _dbContext = dbContext;
            _httpClient = httpClient ?? throw new ArgumentNullException( nameof( httpClient ) );
            _logger = logger ?? throw new ArgumentNullException( nameof( logger ) );
        }

        public bool IsCarModelNameUnique( string name ) {
            return !_dbContext.Cars.Any( b => b.Model == name );
        }

        public async Task<IEnumerable<CarDTO>> GetAllCarsAsync() {
            try {
                var response = await _httpClient.GetAsync( "api/cars" );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<CarDTO>>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while getting all cars." );
                throw;
            }
        }

        public async Task<CarDTO> GetCarByIdAsync( int id ) {
            try {
                var response = await _httpClient.GetAsync( $"api/cars/{id}" );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<CarDTO>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while getting car with ID {id}." );
                throw;
            }
        }

        public async Task<IEnumerable<CarDTO>> SearchCarsAsync( string q ) {
            try {
                var response = await _httpClient.GetAsync( $"api/cars/search/{q}" );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<CarDTO>>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while searching cars with query '{q}'." );
                throw;
            }
        }

        public async Task<CarDTO> CreateCarAsync( Car car ) {
            try {
                var response = await _httpClient.PostAsJsonAsync( "api/cars", car );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<CarDTO>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while creating a car." );
                throw;
            }
        }

        public async Task<CarDTO> UpdateCarAsync( int id, JObject model ) {
            try {
                var response = await _httpClient.PutAsJsonAsync( $"api/cars/{id}", model );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<CarDTO>();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while updating car with ID {id}." );
                throw;
            }
        }

        public async Task DeleteCarAsync( int id ) {
            try {
                var response = await _httpClient.DeleteAsync( $"api/cars/{id}" );
                response.EnsureSuccessStatusCode();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"An error occurred while deleting car with ID {id}." );
                throw;
            }
        }
    }
}
