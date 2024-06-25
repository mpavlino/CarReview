using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model.DTO {
    public class BrandDTO {
        public int ID { get; set; }
        public string Name { get; set; }
        public CountryDTO Country { get; set; }

        public static Expression<Func<Brand, BrandDTO>> SelectorExpression { get; } = p => new BrandDTO() {
            ID = p.ID,
            Name = p.Name,
            Country = new CountryDTO() {
                ID = p.Country.ID,
                Name = p.Country.Name
            },
        };
    }
}
