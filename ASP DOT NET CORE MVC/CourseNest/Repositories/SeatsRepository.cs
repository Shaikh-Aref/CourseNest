using Microsoft.EntityFrameworkCore;

namespace CourseNest.Repositories
{
    public class SeatsRepository : ISeatsRepository
    {
        private readonly ApplicationDbContext _context;

        public SeatsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AvailableSeats?> GetAvailableSteatsByCourseId(int courseId) => await _context.Seatss.FirstOrDefaultAsync(s => s.CourseId== courseId);

        public async Task ManageSeats(SeatsDTO seatsToManage)
        {
            // if there is no seats for given course id, then add new record
            // if there is already seats for given course id, update seats's quantity
            var existingSeats = await GetAvailableSteatsByCourseId(seatsToManage.CourseId);
            if (existingSeats is null)
            {
                var seats = new AvailableSeats { CourseId= seatsToManage.CourseId, SeatCount = seatsToManage.SeatCount };
                _context.Seatss.Add(seats);
            }
            else
            {
                existingSeats.SeatCount = seatsToManage.SeatCount;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SeatsDisplayModel>> GetSeatss(string sTerm = "")
        {
            var seatss = await (from course in _context.Courses
                                join seats in _context.Seatss
                                on course.Id equals seats.CourseId
                                into course_seats
                                from courseSeats in course_seats.DefaultIfEmpty()
                                where string.IsNullOrWhiteSpace(sTerm) || course.CourseName.ToLower().Contains(sTerm.ToLower())
                                select new SeatsDisplayModel
                                {
                                    CourseId= course.Id,
                                    CourseName = course.CourseName,
                                    SeatCount = courseSeats == null ? 0 : courseSeats.SeatCount
                                }
                                ).ToListAsync();
            return seatss;
        }

    }

    public interface ISeatsRepository
    {
        Task<IEnumerable<SeatsDisplayModel>> GetSeatss(string sTerm = "");
        Task<AvailableSeats?> GetAvailableSteatsByCourseId(int courseId);
        Task ManageSeats(SeatsDTO seatsToManage);
    }
}
