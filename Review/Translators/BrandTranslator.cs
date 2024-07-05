using Review.Models.Brand;

namespace Review.Translators {
    public static class BrandTranslator {

        public static Model.Brand TranslateViewModelToModel( BrandViewModel brandViewModel ) {
            Model.Brand brand = new Model.Brand {
                ID = brandViewModel.ID,
                Name = brandViewModel.Name,
                CountryID = brandViewModel.CountryID
            };

            return brand;
        }
    }
}
