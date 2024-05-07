using Review.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model.ViewModels {
    public class ToastrOptionsModel {

        public string Title { get; set; }
        public string Message { get; set; }
        public ToastrTypeEnum Type { get; set; }
        public ToastrPositionEnum PositionClass { get; set; }
        public bool CloseButton { get; set; }
        public bool ProgressBar { get; set; }
        public bool PreventDuplicates { get; set; }
        public bool NewestOnTop { get; set; }
        public int TimeOut { get; set; } = 5000;
        public int ExtendedTimeOut { get; set; } = 1000;

    }
}
