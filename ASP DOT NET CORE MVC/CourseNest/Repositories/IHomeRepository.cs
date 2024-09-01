namespace CourseNest
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Course>> GetCourse(string sTerm = "", int categoryId = 0);
        Task<IEnumerable<Category>> Categories();
    }
}