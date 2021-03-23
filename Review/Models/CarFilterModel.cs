using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review.Models
{
    public class CarFilterModel
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Engine { get; set; }
        public string Country { get; set; }
        public string Reviewer { get; set; }
    }
}
