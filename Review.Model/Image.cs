using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model {
    public class Image {
        public int ID { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        [ForeignKey( nameof( CarReview ) )]
        public int CarReviewId { get; set; }
        public CarReview CarReview { get; set; }
    }
}
