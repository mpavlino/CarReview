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
using System.Net.Http;
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

        [HttpPost( "sync" )]
        public async Task<IActionResult> SyncBrands( [FromBody] List<Brand> brands ) {
            try {
                if( brands == null || !brands.Any() ) {
                    return BadRequest( new { message = "No brands to sync." } );
                }

                foreach( var brand in brands ) {
                    var existingBrand = await _dbContext.Brands.FirstOrDefaultAsync( b => b.Name == brand.Name );

                    if( existingBrand == null ) {
                        // Add new brand
                        _dbContext.Brands.Add( brand );
                    }
                    else {
                        // Update existing brand
                        existingBrand.Name = brand.Name;
                        //existingBrand.CountryID = brand.CountryID;
                        // Update other fields if necessary
                    }
                }

                await _dbContext.SaveChangesAsync();
                return Ok( new { message = "Sync completed successfully." } );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
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

        [HttpGet( "{name}" )]
        public ActionResult<BrandDTO> GetBrandByName( string name ) {
            try {
                var brand = _dbContext.Brands
                    .Where( c => c.Name == name )
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

        #region Model

        [HttpGet( "models/{id:int}" )]
        public async Task<IActionResult> GetModelsByBrandId( int id ) {
            try {
                var model = await _dbContext.Models.Where( m => m.BrandId == id ).ToListAsync();
                if( model != null ) {
                    return Ok( model );
                }
                return BadRequest( new { error = "There was an error while retrieving models with provided brand ID", providedID = id } );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpGet( "model/{id:int}" )]
        public async Task<IActionResult> GetModelById( int id ) {
            try {
                var model = await _dbContext.Models.Include( m => m.Brand ).SingleOrDefaultAsync( m => m.Id == id );
                if( model != null ) {
                    return Ok( model );
                }
                return BadRequest( new { error = "There was an error while retrieving models with provided brand ID", providedID = id } );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpPost( "models/sync" )]
        public async Task<IActionResult> SyncModels( [FromBody] List<Model.Model> models ) {
            try {
                if( models == null || !models.Any() ) {
                    return BadRequest( new { message = "No models to sync." } );
                }

                foreach( var model in models ) {
                    var existingModel = await _dbContext.Models.FirstOrDefaultAsync( m => m.Name == model.Name && m.BrandId == model.BrandId );

                    if( existingModel == null ) {
                        // Add new brand
                        _dbContext.Models.Add( model );
                    }
                    else {
                        // Update existing brand
                        existingModel.Name = model.Name;
                        existingModel.Url = model.Url;
                        // Update other fields if necessary
                    }
                }

                await _dbContext.SaveChangesAsync();
                return Ok( new { message = "Sync completed successfully." } );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        #endregion
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
