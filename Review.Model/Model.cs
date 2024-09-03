using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model {
    public class Model {

        [Key]
        public int Id { get; set; }

        [Required( ErrorMessage = "Brand name is required." )]
        public string Name { get; set; }

        [ForeignKey( nameof( Brand ) )]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
