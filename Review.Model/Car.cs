using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Review.Model
{
    public class Car
    {
        public int ID { get; set; }

        [ForeignKey(nameof(Brand))]
        public int? BrandID { get; set; }
        public Brand Brand { get; set; }

        [StringLength(200)]
        public string Model { get; set; }

        public string Engine { get; set; }
        public int TopSpeed { get; set; }
        public double Acceleration { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ModelYear { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(Country))]
        public int? CountryID { get; set; }
        public Country Country { get; set; }

        [ForeignKey(nameof(Reviewer))]
        public int? ReviewerID { get; set; }
        public Reviewer Reviewer { get; set; }
    }
}
