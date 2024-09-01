namespace CourseNest.Models.DTOs
{
    public class CourseDisplayModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string STerm { get; set; } = "";
        public int CategoryId { get; set; } = 0;
    }
}
