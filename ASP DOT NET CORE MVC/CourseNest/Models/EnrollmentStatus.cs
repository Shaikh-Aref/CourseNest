using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseNest.Models
{
    [Table("EnrollmentStatus")]
    public class EnrollmentStatus
    {
        public int Id { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required,MaxLength(20)]
        public string ?EnrollmentStatusName{ get; set; }
    }
}
