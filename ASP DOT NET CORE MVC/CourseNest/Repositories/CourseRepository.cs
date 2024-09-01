using Microsoft.EntityFrameworkCore;

namespace CourseNest.Repositories
{
    public interface ICourseRepository
    {
        Task AddCourse(Course course);
        Task DeleteCourse(Course course);
        Task<Course?> GetCourseById(int id);
        Task<IEnumerable<Course>> GetCourse();
        Task UpdateCourse(Course course);
    }

    public class courseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public courseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourse(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task<Course?> GetCourseById(int id) => await _context.Courses.FindAsync(id);

        public async Task<IEnumerable<Course>> GetCourse() => await _context.Courses.Include(a=>a.Category).ToListAsync();
    }
}
