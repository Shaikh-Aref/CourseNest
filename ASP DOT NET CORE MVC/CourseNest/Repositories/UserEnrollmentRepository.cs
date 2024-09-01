using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseNest.Repositories
{
    public class UserEnrollmentRepository : IUserEnrollmentRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;


        public UserEnrollmentRepository(ApplicationDbContext db,
            UserManager<IdentityUser> userManager,
             IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task ChangeEnrollmentStatus(UpdateEnrollmentStatusModel data)
        {
            var enrollment = await _db.Enrollments.FindAsync(data.EnrollmentId);
            if (enrollment == null)
            {
                throw new InvalidOperationException($"enrollment withi id:{data.EnrollmentId} does not found");
            }
            enrollment.EnrollmentStatusId = data.EnrollmentStatusId;
            await _db.SaveChangesAsync();
        }

        public async Task<Enrollment?> GetEnrollmentById(int id)
        {
            return await _db.Enrollments.FindAsync(id);
        }

        public async Task<IEnumerable<EnrollmentStatus>> GetEnrollmentStatuses()
        {
            return await _db.enrollmentStatuses.ToListAsync();
        }

        public async Task TogglePaymentStatus(int enrollmentId)
        {
            var enrollment = await _db.Enrollments.FindAsync(enrollmentId);
            if (enrollment == null)
            {
                throw new InvalidOperationException($"enrollment withi id:{enrollmentId} does not found");
            }
            enrollment.IsPaid = enrollment.IsPaid;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Enrollment>> UserEnrollments(bool getAll = false)
        {
            var enrollments = _db.Enrollments
                           .Include(x => x.EnrollmentStatus)
                           .Include(x => x.EnrollmentDetail)
                           .ThenInclude(x => x.Course)
                           .ThenInclude(x => x.Category).AsQueryable();
            if (!getAll)
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged-in");
                enrollments = enrollments.Where(a => a.UserId== userId);
                return await enrollments.ToListAsync();
            }

            return await enrollments.ToListAsync();
        }

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
