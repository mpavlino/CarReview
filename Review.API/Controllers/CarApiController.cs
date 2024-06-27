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
using System.Threading.Tasks;

namespace Review.Controllers {
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

        [HttpGet( "pretraga/{q}" )]
        public ActionResult<IEnumerable<CarDTO>> SearchCars( string q ) {
            try {
                var cars = _dbContext.Cars
                    .Where( c => c.Brand.Name.Contains( q ) )
                    .Select( CarDTO.SelectorExpression )
                    .ToList();

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
                    c.Brand.CountryID = c.Brand.CountryID;
                    c.Brand.Country = null;
                    _dbContext.Cars.Add( c );
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
