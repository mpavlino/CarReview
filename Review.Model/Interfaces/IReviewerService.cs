using Newtonsoft.Json.Linq;
using Review.Model;
using Review.Model.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Review.Model.Interfaces {
    public interface IReviewerService {
        Task<IEnumerable<Reviewer>> GetAllReviewersAsync();
        Task<Reviewer> GetReviewerByIdAsync( int id );
        Task<Reviewer> CreateReviewerAsync( Reviewer reviewer );
        Task<Reviewer> UpdateReviewerAsync( int id, Reviewer model );
        Task DeleteReviewerAsync( int id );
    }
}
