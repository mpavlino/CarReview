using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model {
    public class CarReview {

        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }

        [DataType( DataType.Date )]
        [DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true )]
        public DateTime CreatedOn { get; set; }

        [ForeignKey( nameof( Reviewer ) )]
        public int ReviewerId { get; set; }   
        public Reviewer Reviewer { get; set; }

        [ForeignKey( nameof( Car ) )]
        public int CarID { get; set; }
        public Car Car { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
