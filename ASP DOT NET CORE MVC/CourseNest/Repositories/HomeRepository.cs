

using Microsoft.EntityFrameworkCore;

namespace CourseNest.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> Categories()
        {
            return await _db.Categories.ToListAsync();
        }
        public async Task<IEnumerable<Course>> GetCourse(string sTerm = "", int categoryId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Course> courses = await (from course in _db.Courses
                         join category in _db.Categories
                         on course.CategoryId equals category.Id
                         join seats in _db.Seatss
                         on course.Id equals seats.CourseId
                         into course_seats
                         from courseWithSeats in course_seats.DefaultIfEmpty()
                         where string.IsNullOrWhiteSpace(sTerm) || (course != null && course.CourseName.ToLower().StartsWith(sTerm))
                         select new Course
                         {
                             Id = course.Id,
                             Image = course.Image,
                             InstructorName = course.InstructorName,
                             CourseName = course.CourseName,
                             CategoryId = course.CategoryId,
                             CourseFee = course.CourseFee,
                             CategoryName = category.CategoryName,
                             SeatCount=courseWithSeats==null? 0:courseWithSeats.SeatCount
                         }
                         ).ToListAsync();
            if (categoryId > 0)
            {

                courses = courses.Where(a => a.CategoryId == categoryId).ToList();
            }
            return courses;

        }
    }
}
