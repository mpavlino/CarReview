using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Review.Controllers;
using Review.DAL;
using Review.Model;
using Review.Model.DTO;

namespace Review.API.Controllers {

    [Route( "/api/brand" )]
    [ApiController]
    public class BrandApiController : Controller {

        private CarManagerDbContext _dbContext;

        public BrandApiController( CarManagerDbContext dbContext ) {
            this._dbContext = dbContext;
        }

        [Route( "" )]
        public ActionResult<List<BrandDTO>> Get() {
            List<BrandDTO> brands = this._dbContext.Brands
                .Select( BrandDTO.SelectorExpression )
                .ToList();

            return brands;
        }

        [Route( "{id:int}" )]
        public ActionResult<BrandDTO> Get( int id ) {
            var brand = this._dbContext.Brands
                .Where( c => c.ID == id )
                .Select( BrandDTO.SelectorExpression )
                .FirstOrDefault();

            return brand;
        }


        [Route( "" )]
        [HttpPost]
        public ActionResult<BrandDTO> Post( Brand b ) {
            if( ModelState.IsValid ) {
                b.CountryID = b.CountryID;
                b.Country = null;
                this._dbContext.Brands.Add( b );
                this._dbContext.SaveChanges();

                return Get( b.ID );
            }
            return BadRequest( ModelState );
        }

        [Route( "{id:int}" )]
        [HttpPut]
        public async Task<ActionResult<BrandDTO>> Put( int id, [FromBody] JObject model ) {
            var valueProvider = new ObjectSourceValueProvider( model );

            Brand existing = _dbContext.Brands.FirstOrDefault( p => p.ID == id );
            if( existing != null && ModelState.IsValid && await TryUpdateModelAsync( existing, "", valueProvider ) ) {
                this._dbContext.SaveChanges();
                return Get( id );
            }
            if( existing == null ) {
                ModelState.AddModelError( "id", "Invalid client ID" );
            }
            return BadRequest( ModelState );
        }
    }
}
