using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseNest.Controllers
{
    [Authorize]
    public class UserEnrollmentController : Controller
    {
        private readonly IUserEnrollmentRepository _userEnrollmentRepo;

        public UserEnrollmentController(IUserEnrollmentRepository userEnrollmentRepo)
        {
            _userEnrollmentRepo = userEnrollmentRepo;
        }
        public async Task<IActionResult> UserEnrollments()
        {
            var enrollments = await _userEnrollmentRepo.UserEnrollments();
            return View(enrollments);
        }
    }
}
