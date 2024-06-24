using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model.Interfaces {
    public interface ICarService {
        bool IsCarModelNameUnique( string name );
    }
}
