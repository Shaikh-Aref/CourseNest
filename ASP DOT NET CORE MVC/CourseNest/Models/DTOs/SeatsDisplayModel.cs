namespace CourseNest.Models.DTOs
{
    public class SeatsDisplayModel
    {
        public int Id { get; set; }
        public int CourseId{ get; set; }
        public int SeatCount { get; set; }
        public string? CourseName { get; set; }
    }
}
