using Review.Model;
using Review.Model.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Review.Model.Interfaces {
    public interface IReviewerService {
        Task<IEnumerable<ReviewerDTO>> GetAllReviewersAsync();
        Task<ReviewerDTO> GetReviewerByIdAsync( int id );
        Task<ReviewerDTO> CreateReviewerAsync( Reviewer reviewer );
        Task<ReviewerDTO> UpdateReviewerAsync( int id, Reviewer reviewer );
        Task DeleteReviewerAsync( int id );
    }
}
