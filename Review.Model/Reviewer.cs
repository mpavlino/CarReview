using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Review.Model
{
    public class Reviewer
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string FirstName { get; set; }
        [Required, StringLength(100)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string About { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public virtual ICollection<Car> Cars { get; set; }
    }
}

