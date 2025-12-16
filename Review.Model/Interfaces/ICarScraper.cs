using Review.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model.Interfaces {
    public interface ICarScraper {
        Task<List<CarManDTO>> GetBrandsAsync();
        Task<List<CarModDTO>> GetModelsAsync();
    }
}
