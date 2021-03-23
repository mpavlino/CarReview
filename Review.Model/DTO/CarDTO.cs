using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Review.Model.DTO
{
    public class CarDTO
    {
        public int ID { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public CountryDTO Country { get; set; }
        public string Engine { get; set; }

        public static Expression<Func<Car, CarDTO>> SelectorExpression { get; } = p => new CarDTO()
        {
            ID = p.ID,
            BrandName = p.Brand.Name,
            ModelName= p.Model,
            Country = p.CountryID == null ? null : new CountryDTO()
            {
                ID = p.Country.ID,
                Name = p.Country.Name
            },
            Engine = p.Engine
        };
    }
}
