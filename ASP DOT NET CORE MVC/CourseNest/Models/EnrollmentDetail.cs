using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseNest.Models
{
    [Table("EnrollmentDetail")]
    public class EnrollmentDetail
    {
        public int Id { get; set; }
        [Required]
        public int EnrollmentId{ get; set; }
        [Required]
        public int CourseId{ get; set; }
        [Required]
        public int SeatCount { get; set; }
        [Required]
        public double CourseUnitFee{ get; set; }
        public Enrollment Enrollment{ get; set; }
        public Course Course { get; set; }
    }
}
