using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Review.Model.DTO;
using Review.Model;
using Review.DAL;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace Review.API.Controllers {
    [Authorize]
    [Route( "api/reviewers" )]
    [ApiController]
    public class ReviewerApiController : ControllerBase {

        private readonly CarManagerDbContext _dbContext;

        public ReviewerApiController( CarManagerDbContext dbContext ) {
            _dbContext = dbContext;
        }

        #region Reviewers
        [HttpGet]
        public ActionResult<IEnumerable<ReviewerDTO>> GetAllReviewers() {
            try {
                List<ReviewerDTO> reviewers = _dbContext.Reviewers
                    .Select( ReviewerDTO.SelectorExpression )
                    .ToList();

                return Ok( reviewers );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpGet( "{id:int}" )]
        public ActionResult<ReviewerDTO> GetReviewerById( int id ) {
            try {
                var reviewer = _dbContext.Reviewers
                    .Where( r => r.ID == id )
                    .Select( ReviewerDTO.SelectorExpression )
                    .FirstOrDefault();

                if( reviewer == null ) {
                    return NotFound( new { message = "Reviewer not found" } );
                }

                return Ok( reviewer );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpPost]
        public ActionResult<ReviewerDTO> CreateReviewer( [FromBody] Reviewer reviewer ) {
            try {
                if( ModelState.IsValid ) {
                    _dbContext.Reviewers.Add( reviewer );
                    _dbContext.SaveChanges();

                    return CreatedAtAction( nameof( GetReviewerById ), new { id = reviewer.ID }, GetReviewerById( reviewer.ID ).Value );
                }

                return BadRequest( ModelState );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpPut( "{id:int}" )]
        public async Task<ActionResult<ReviewerDTO>> UpdateReviewer( int id, [FromBody] ReviewerDTO reviewer ) {
            try {

                Reviewer existing = _dbContext.Reviewers.FirstOrDefault( r => r.ID == id );
                if( existing != null && ModelState.IsValid ) {
                    _dbContext.Entry( existing ).CurrentValues.SetValues( reviewer );
                    await _dbContext.SaveChangesAsync();
                    return GetReviewerById( id );
                }

                if( existing == null ) {
                    ModelState.AddModelError( "id", "Invalid reviewer ID" );
                }

                return BadRequest( ModelState );
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }

        [HttpDelete( "{id:int}" )]
        public IActionResult DeleteReviewer( int id ) {
            try {
                var existing = _dbContext.Reviewers.FirstOrDefault( r => r.ID == id );
                if( existing != null ) {
                    _dbContext.Entry( existing ).State = EntityState.Deleted;
                    _dbContext.SaveChanges();
                    return Ok();
                }
                else {
                    return NotFound( new { error = "Reviewer not found", providedID = id } );
                }
            }
            catch( Exception ex ) {
                return StatusCode( 500, new { message = ex.Message } );
            }
        }
        #endregion

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
}
