using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseNest.Models
{
    [Table("EnrollmentCartDetail")]
    public class EnrollmentCartDetail
    {
        public int Id { get; set; }
        [Required]
        public int EnrollmentCartId{ get; set; }
        [Required]
        public int CourseId{ get; set; }
        [Required]
        public int SeatCount { get; set; }
        [Required]
        public double CourseUnitFee{ get; set; }   
        public Course Course { get; set; }
        public EnrollmentCart EnrollmentCart{ get; set; }
    }
}
