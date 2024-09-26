using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model.DTO {
    public class ModelDTO {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int BrandId { get; set; }
        public BrandDTO Brand { get; set; }

        public static Expression<Func<Model, ModelDTO>> SelectorExpression { get; } = p => new ModelDTO() {
            Id = p.Id,
            Name = p.Name,
            Url = p.Url,
            BrandId = p.BrandId,
            Brand = p.BrandId == null ? null : new BrandDTO() {
                ID = p.Brand.ID,
                Name = p.Brand.Name,
                CountryID = p.Brand.CountryID
            },
        };
    }
}
