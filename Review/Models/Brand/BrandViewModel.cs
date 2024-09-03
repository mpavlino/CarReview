using Review.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Review.Models.Brand {
    public class BrandViewModel {

        public BrandViewModel() { }
        public BrandViewModel(Model.Brand brand) { 
            ID = brand.ID;
            Name = brand.Name;
            CountryID = brand.CountryID;
        }

        public int ID { get; set; }
        [Required( ErrorMessage = "Brand name is required." )]
        [UniqueBrandName( ErrorMessage = "A brand with this name already exists." )]
        public string Name { get; set; }
        public int? CountryID { get; set; }
        public List<ModelViewModel> Models { get; set; } = new List<ModelViewModel>();
    }
}
