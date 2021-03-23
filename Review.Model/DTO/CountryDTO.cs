using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Review.Model.DTO
{
    public class CountryDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static Expression<Func<Country, CountryDTO>> SelectorExpression { get; } = p => new CountryDTO()
        {
            ID = p.ID,
            Name = p.Name
        };
    }
}
