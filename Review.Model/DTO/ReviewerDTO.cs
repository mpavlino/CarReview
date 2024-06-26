using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Review.Model.DTO {
    public class ReviewerDTO {

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string About { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public static Expression<Func<Reviewer, ReviewerDTO>> SelectorExpression { get; } = p => new ReviewerDTO() {
            ID = p.ID,
            FirstName = p.FirstName,
            LastName = p.LastName,
            DateOfBirth = p.DateOfBirth,
            Gender = p.Gender,
            About = p.About
        };
    }
}
