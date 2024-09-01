using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseNest.Controllers
{
    [Authorize(Roles=nameof(Roles.Admin))]
    public class AvailableSeatsController : Controller
    {
        private readonly ISeatsRepository _seatsRepo;

        public AvailableSeatsController(ISeatsRepository seatsRepo)
        {
            _seatsRepo = seatsRepo;
        }

        public async Task<IActionResult> Index(string sTerm="")
        {
            var seatss=await _seatsRepo.GetSeatss(sTerm);
            return View(seatss);
        }

        public async Task<IActionResult> ManangeSeats(int courseId)
        {
            var existingSeats = await _seatsRepo.GetAvailableSteatsByCourseId(courseId);
            var seats = new SeatsDTO
            {
                CourseId= courseId,
                SeatCount = existingSeats != null
            ? existingSeats.SeatCount : 0
            };
            return View(seats);
        }

        [HttpPost]
        public async Task<IActionResult> ManangeSeats(SeatsDTO seats)
        {
            if (!ModelState.IsValid)
                return View(seats);
            try
            {
                await _seatsRepo.ManageSeats(seats);
                TempData["successMessage"] = "AvailableSeats is updated successfully.";
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Something went wrong!!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
