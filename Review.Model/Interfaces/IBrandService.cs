using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model.Interfaces {
    public interface IBrandService {
        bool IsBrandNameUnique( string name );
    }

}
