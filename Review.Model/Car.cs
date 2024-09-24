using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Review.Model {
    public class Car {
        public int ID { get; set; }

        [ForeignKey( nameof( Brand ) )]
        public int? BrandID { get; set; }
        public Brand Brand { get; set; }

        [ForeignKey( nameof( Model ) )]
        public int? ModelID { get; set; }
        public Model Model { get; set; }

        public string Generation { get; set; }

        [DataType( DataType.Date )]
        [DisplayFormat( DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true )]
        public DateTime ModelYearFrom { get; set; }

        [DataType( DataType.Date )]
        [DisplayFormat( DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true )]
        public DateTime? ModelYearTo { get; set; }
        public string Description { get; set; }

        [ForeignKey( nameof( Reviewer ) )]
        public int? ReviewerID { get; set; }
        public Reviewer Reviewer { get; set; }
        public decimal Rating { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        // Navigation properties
        public List<Engine> Engines { get; set; }
        public List<CarReview> CarReviews { get; set; }
    }
}
