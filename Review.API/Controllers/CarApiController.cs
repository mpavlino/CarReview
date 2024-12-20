﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Review.DAL;
using Review.Model;
using Review.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Review.Controllers {
    [Authorize]
    [Route( "api/cars" )]
    [ApiController]
    public class CarApiController : ControllerBase {
        private readonly CarManagerDbContext _dbContext;

        public CarApiController( CarManagerDbContext dbContext ) {
            _dbContext = dbContext;
        }

        #region Cars
        [HttpGet]
        public ActionResult<IEnumerable<CarDTO>> GetAllCars() {
            try {
                List<CarDTO> cars = _dbContext.Cars
                    .Select( CarDTO.SelectorExpression )
                    .ToList();

                return Ok( cars );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpGet( "{id:int}" )]
        public ActionResult<CarDTO> GetCarById( int id ) {
            try {
                var car = _dbContext.Cars
                    .Where( c => c.ID == id )
                    .Select( CarDTO.SelectorExpression )
                    .FirstOrDefault();

                if( car == null ) {
                    return NotFound( new { message = "Car not found" } );
                }

                return Ok( car );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpPost( "search" )]
        public ActionResult<IEnumerable<CarDTO>> SearchCars( CarFilterModel filter ) {
            try {
                var carQuery = this._dbContext.Cars.Include( c => c.Brand ).ThenInclude( c => c.Country ).Include( c => c.Reviewer ).Include( c => c.Engines ).AsQueryable();

                //Primjer iterativnog građenja upita - dodaje se "where clause" samo u slučaju da je parametar doista proslijeđen.
                //To rezultira optimalnijim stablom izraza koje se kvalitetnije potencijalno prevodi u SQL
                if( !string.IsNullOrWhiteSpace( filter.Brand ) )
                    carQuery = carQuery.Where( p => p.BrandID != null && p.Brand.ID.ToString() == filter.Brand );

                if( !string.IsNullOrWhiteSpace( filter.Model ) )
                    carQuery = carQuery.Where( p => p.ModelID != null && p.Model.Id.ToString() == filter.Model );

                if( !string.IsNullOrWhiteSpace( filter.Engine ) )
                    carQuery = carQuery.Where( p => p.Engines.Any( e => e.Name.ToLower().Contains( filter.Engine.ToLower() ) ) );

                if( !string.IsNullOrWhiteSpace( filter.Country ) )
                    carQuery = carQuery.Where( p => p.Brand.CountryID != null && p.Brand.Country.ID.ToString() == filter.Country );

                if( !string.IsNullOrWhiteSpace( filter.Reviewer ) )
                    carQuery = carQuery.Where( p => p.ReviewerID != null && ( p.Reviewer.FirstName + " " + p.Reviewer.LastName ).ToLower().Contains( filter.Reviewer.ToLower() ) );

                var cars = carQuery.Select( CarDTO.SelectorExpression ).ToList();

                return Ok( cars );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpPost( "query" )]
        public ActionResult<IEnumerable<CarDTO>> SearchCarsByText( [FromBody] string query ) {
            try {
                var carQuery = this._dbContext.Cars
                                .Include( c => c.Brand )
                                .ThenInclude( c => c.Country )
                                .Include( c => c.Reviewer )
                                .AsQueryable();

                if( !string.IsNullOrWhiteSpace( query ) ) {
                    var loweredQuery = query.ToLower();
                    carQuery = carQuery.Where( p => (p.Brand.Name != null && p.Brand.Name.ToLower().Contains( loweredQuery )) ||
                                                    (p.Model.Name != null && p.Model.Name.ToLower().Contains( loweredQuery )) ||
                                                    p.Generation.ToLower().Contains( loweredQuery ) ||
                                                    (p.Brand.Country.Name != null && p.Brand.Country.Name.ToLower().Contains( loweredQuery )) );
                }

                var cars = carQuery.Select( CarDTO.SelectorExpression ).ToList();

                return Ok( cars );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }


        [HttpPost]
        public ActionResult<CarDTO> CreateCar( [FromBody] Car c ) {
            try {
                if( ModelState.IsValid ) {
                    _dbContext.Cars.Add( c );

                    if( c.Engines != null && c.Engines.Count > 0 ) {
                        foreach( var engine in c.Engines ) {
                            _dbContext.Engines.Add( engine );
                        }
                    }
                    _dbContext.SaveChanges();

                    return CreatedAtAction( nameof( GetCarById ), new { id = c.ID }, GetCarById( c.ID ).Value );
                }

                return BadRequest( ModelState );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpPut( "{id:int}" )]
        public async Task<ActionResult<CarDTO>> UpdateCar( int id, [FromBody] Car car ) {
            try {

                Car existing = _dbContext.Cars.FirstOrDefault( p => p.ID == id );
                if( existing != null && ModelState.IsValid ) {
                    _dbContext.Entry( existing ).CurrentValues.SetValues( car );
                    await _dbContext.SaveChangesAsync();
                    return GetCarById( id );
                }

                if( existing == null ) {
                    ModelState.AddModelError( "id", "Invalid car ID" );
                }

                return BadRequest( ModelState );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpDelete( "{id:int}" )]
        public IActionResult DeleteCar( int id ) {
            try {
                var existing = _dbContext.Cars.FirstOrDefault( p => p.ID == id );
                if( existing != null ) {
                    _dbContext.Entry( existing ).State = EntityState.Deleted;
                    _dbContext.SaveChanges();
                    return Ok();
                }
                else {
                    return NotFound( new { error = "Car not found", providedID = id } );
                }
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }
        #endregion

        #region CarReview


        [HttpGet( "reviews/{id:int}" )]
        public ActionResult<List<CarReviewDTO>> GetCarReviewsByCarId( int id ) {
            try {
                var reviews = _dbContext.CarReviews
                    .Where( r => r.CarID == id )
                    .Select( CarReviewDTO.SelectorExpression )
                    .ToList();

                if( reviews == null || reviews.Count == 0 ) {
                    return NotFound( new { message = "Reivews were not found" } );
                }

                return Ok( reviews );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpGet( "review/{id:int}" )]
        public ActionResult<CarReviewDTO> GetCarReviewById( int id ) {
            try {
                var review = _dbContext.CarReviews
                    .Where( r => r.ID == id )
                    .Select( CarReviewDTO.SelectorExpression )
                    .FirstOrDefault();

                if( review == null ) {
                    return NotFound( new { message = "Reivews were not found" } );
                }

                return Ok( review );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpPost( "review" )]
        public ActionResult<CarReviewDTO> CreateCarReview( [FromBody] CarReview c ) {
            try {
                if( ModelState.IsValid ) {
                    _dbContext.CarReviews.Add( c );
                    foreach( var image in c.Images ) {
                        _dbContext.Images.Add( image );
                    }
                    _dbContext.SaveChanges();

                    return CreatedAtAction( nameof( GetCarReviewById ), new { id = c.ID }, GetCarReviewById( c.ID ).Value );
                }

                return BadRequest( ModelState );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpPut( "review/{id:int}" )]
        public async Task<ActionResult<CarReviewDTO>> UpdateCarReview( int id, [FromBody] CarReview carReview ) {
            try {

                CarReview existing = _dbContext.CarReviews.Include(c => c.Images).FirstOrDefault( p => p.ID == id );
                if( existing != null && ModelState.IsValid ) {
                    // Update the properties of the existing CarReview
                    _dbContext.Entry( existing ).CurrentValues.SetValues( carReview );

                    // Remove images that are no longer present
                    var imagesToRemove = existing.Images.Where( ei => !carReview.Images.Any( ci => ci.ID == ei.ID ) ).ToList();
                    foreach( var image in imagesToRemove ) {
                        existing.Images.Remove( image );
                    }

                    // Handle Images
                    if( carReview.Images != null && carReview.Images.Any() ) {

                        // Add or update images
                        foreach( var newImage in carReview.Images ) {
                            var existingImage = existing.Images.FirstOrDefault( ei => ei.ID == newImage.ID );
                            if( existingImage != null && existingImage.ID > 0 ) {
                                // Update existing image
                                _dbContext.Entry( existingImage ).CurrentValues.SetValues( newImage );
                            }
                            else {
                                // Add new image
                                existing.Images.Add( newImage );
                            }
                        }
                    }

                    await _dbContext.SaveChangesAsync();
                    return GetCarReviewById( id );
                }

                if( existing == null ) {
                    ModelState.AddModelError( "id", "Invalid car review ID" );
                }

                return BadRequest( ModelState );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpDelete( "review/{id:int}" )]
        public IActionResult DeleteCarReview( int id ) {
            try {
                var existing = _dbContext.CarReviews.FirstOrDefault( p => p.ID == id );
                if( existing != null ) {
                    _dbContext.Entry( existing ).State = EntityState.Deleted;
                    _dbContext.SaveChanges();
                    return Ok();
                }
                else {
                    return NotFound( new { error = "Car review not found", providedID = id } );
                }
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        #endregion
    }

    public class ObjectSourceValueProvider : IValueProvider {
        private readonly JObject _source;

        public ObjectSourceValueProvider( JObject source ) {
            _source = source ?? throw new ArgumentNullException( nameof( source ) );
        }

        public bool ContainsPrefix( string prefix ) {
            return _source.Properties().Any( p => p.Name.Equals( prefix, StringComparison.OrdinalIgnoreCase ) );
        }

        public ValueProviderResult GetValue( string key ) {
            if( _source.TryGetValue( key, StringComparison.OrdinalIgnoreCase, out var value ) ) {
                return new ValueProviderResult( value.ToString() );
            }
            return ValueProviderResult.None;
        }
    }
}
