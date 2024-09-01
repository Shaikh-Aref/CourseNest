using System.ComponentModel.DataAnnotations;

namespace CourseNest.Models.DTOs
{
    public class SeatsDTO
    {
        public int CourseId{ get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "SeatCount must be a non-negative value.")]
        public int SeatCount { get; set; }
    }
}
