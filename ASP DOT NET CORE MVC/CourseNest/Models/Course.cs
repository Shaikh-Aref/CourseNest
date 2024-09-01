using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseNest.Models
{
    [Table("Course")]
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? CourseName { get; set; }

        [Required]
        [MaxLength(40)]
        public string? InstructorName { get; set; }
        [Required]
        public double CourseFee { get; set; }
        public string? Image { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<EnrollmentDetail> EnrollmentDetail { get; set; }
        public List<EnrollmentCartDetail> EnrollmentCartDetail { get; set; }
        public AvailableSeats AvailableSeats { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }
        [NotMapped]
        public int SeatCount { get; set; }


    }
}
