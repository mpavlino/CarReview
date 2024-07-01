using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Review.Model
{
    public class AppUser : IdentityUser
    {
        [RegularExpression("[0-9]{11}")]
        public string OIB { get; set; }
    }
}
