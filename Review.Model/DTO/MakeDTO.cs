using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model.DTO {
    public class MakeDTO {
        public int MakeId { get; set; }
        public int? Model_ID { get; set; }
        public int? VehicleTypeId { get; set; }
        public string MakeName { get; set; }
        public string Model_Name { get; set; }
        public string VehicleTypeName { get; set; }
    }
}
