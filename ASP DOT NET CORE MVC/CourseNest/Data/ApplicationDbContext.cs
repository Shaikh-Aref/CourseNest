using CourseNest.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseNest.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<EnrollmentCart> EnrollmentCarts { get; set; }
        public DbSet<EnrollmentCartDetail> EnrollmentCartDetails { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<EnrollmentDetail> EnrollmentDetails { get; set; }

        public DbSet<EnrollmentStatus> enrollmentStatuses { get; set; }
        public DbSet<AvailableSeats> Seatss { get; set; }

    }
}