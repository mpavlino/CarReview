using Review.Model.Enums;
using Review.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model.Helpers {
    public static class ToastrHelper {

        public static ToastrOptionsModel SuccessMessage( string title, string message, ToastrPositionEnum positon ) {
            var tostr = GetDefaultOptions( ToastrTypeEnum.success );

            tostr.Title = title;
            tostr.Message = message;
            tostr.PositionClass = positon;
            return tostr;
        }

        public static ToastrOptionsModel ErrorMessage( string title, string message, ToastrPositionEnum positon ) {
            var tostr = GetDefaultOptions( ToastrTypeEnum.error );

            tostr.Title = title;
            tostr.Message = message;
            tostr.PositionClass = positon;
            tostr.TimeOut = tostr.ExtendedTimeOut = 0;
            return tostr;
        }
        public static ToastrOptionsModel InfoMessage( string title, string message, ToastrPositionEnum positon ) {
            var tostr = GetDefaultOptions( ToastrTypeEnum.info );

            tostr.Title = title;
            tostr.Message = message;
            tostr.PositionClass = positon;
            return tostr;
        }
        public static ToastrOptionsModel WarningMessage( string title, string message, ToastrPositionEnum positon ) {
            var tostr = GetDefaultOptions( ToastrTypeEnum.warning );

            tostr.Title = title;
            tostr.Message = message;
            tostr.PositionClass = positon;
            tostr.TimeOut = tostr.ExtendedTimeOut = 0;
            return tostr;
        }


        private static ToastrOptionsModel GetDefaultOptions( ToastrTypeEnum type ) {

            return new ToastrOptionsModel() {
                CloseButton = true,
                NewestOnTop = true,
                PreventDuplicates = true,
                ProgressBar = true,
                Type = type
            };
        }

    }
}
