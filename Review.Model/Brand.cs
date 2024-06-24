using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Review.Model
{
    public class Brand
    {
        [Key]
        public int ID { get; set; }

        [Required( ErrorMessage = "Brand name is required." )]
        [UniqueBrandName( ErrorMessage = "A brand with this name already exists." )]
        public string Name { get; set; }

        [ForeignKey( nameof( Country ) )]
        public int CountryID { get; set; }
        public Country Country { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
