using System.ComponentModel.DataAnnotations.Schema;

namespace CourseNest.Models
{
    [Table("AvailableSeats")]
    public class AvailableSeats
    {
        public int Id { get; set; }
        public int CourseId{ get; set; }
        public int SeatCount { get; set; }

        public Course? Course { get; set; }
    }
}
