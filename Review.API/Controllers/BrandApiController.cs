using Microsoft.AspNetCore.Authorization;
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
using System.Runtime.ConstrainedExecution;

namespace Review.API.Controllers {
    [Authorize]
    [Route( "api/brands" )]
    [ApiController]
    public class BrandApiController : ControllerBase {
        private readonly CarManagerDbContext _dbContext;

        public BrandApiController( CarManagerDbContext dbContext ) {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<BrandDTO>> GetAllBrands() {
            try {
                List<BrandDTO> brands = _dbContext.Brands
                    .Select( BrandDTO.SelectorExpression )
                    .ToList();

                return brands;
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpGet( "{id:int}" )]
        public ActionResult<BrandDTO> GetBrandById( int id ) {
            try {
                var brand = _dbContext.Brands
                    .Where( c => c.ID == id )
                    .Select( BrandDTO.SelectorExpression )
                    .FirstOrDefault();

                if( brand == null ) {
                    return NotFound( new { message = "Brand not found" } );
                }

                return brand;
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpPost]
        public ActionResult<BrandDTO> CreateBrand( [FromBody] Brand b ) {
            try {
                if( ModelState.IsValid ) {
                    b.CountryID = b.CountryID;
                    b.Country = null;
                    _dbContext.Brands.Add( b );
                    _dbContext.SaveChanges();

                    return GetBrandById( b.ID );
                }
                return BadRequest( ModelState );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpPut( "{id:int}" )]
        public async Task<ActionResult<BrandDTO>> UpdateBrand( int id, [FromBody] BrandDTO brand ) {
            try {

                Brand existing = _dbContext.Brands.FirstOrDefault( p => p.ID == id );
                if( existing != null && ModelState.IsValid ) {
                    _dbContext.Entry( existing ).CurrentValues.SetValues( brand );
                    await _dbContext.SaveChangesAsync();
                    return GetBrandById( id );
                }
                if( existing == null ) {
                    ModelState.AddModelError( "id", "Invalid brand ID" );
                }
                return BadRequest( ModelState );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpDelete( "{id:int}" )]
        public IActionResult DeleteBrand( int id ) {
            try {
                var existing = _dbContext.Brands.FirstOrDefault( p => p.ID == id );
                if( existing != null ) {
                    _dbContext.Entry( existing ).State = EntityState.Deleted;
                    _dbContext.SaveChanges();
                    return Ok();
                }
                else {
                    return BadRequest( new { error = "Unable to locate brand with provided ID", providedID = id } );
                }
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }
    }

    public class ObjectSourceValueProvider : IValueProvider {
        private JObject _x;

        public ObjectSourceValueProvider( JObject x ) {
            this._x = x;
        }

        public bool ContainsPrefix( string prefix ) {
            return _x.Properties().Any( p => p.Name == prefix );
        }

        public ValueProviderResult GetValue( string key ) {
            var prop = _x.Properties().Where( p => p.Name.Equals( key, StringComparison.OrdinalIgnoreCase ) ).FirstOrDefault();

            if( prop == null ) {
                return ValueProviderResult.None;
            }
            return new ValueProviderResult( prop.Value.ToString() );
        }
    }
}
