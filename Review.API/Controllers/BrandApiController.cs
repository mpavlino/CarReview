using Microsoft.AspNetCore.Mvc;
using Review.DAL;
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
    }
}
