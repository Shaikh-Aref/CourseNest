using CourseNest.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseNest.Controllers;

[Authorize(Roles = nameof(Roles.Admin))]
public class AdminOperationsController : Controller
{
    private readonly IUserEnrollmentRepository _userEnrollmentRepository;
    public AdminOperationsController(IUserEnrollmentRepository userEnrollmentRepository)
    {
        _userEnrollmentRepository = userEnrollmentRepository;
    }

    public async Task<IActionResult> AllEnrollments()
    {
        var enrollments = await _userEnrollmentRepository.UserEnrollments(true);
        return View(enrollments);
    }

    public async Task<IActionResult> TogglePaymentStatus(int enrollmentId)
    {
        try
        {
            await _userEnrollmentRepository.TogglePaymentStatus(enrollmentId);
        }
        catch (Exception ex)
        {
            // log exception here
        }
        return RedirectToAction(nameof(AllEnrollments));
    }

    public async Task<IActionResult> UpdateEnrollmentStatus(int enrollmentId)
    {
        var enrollment = await _userEnrollmentRepository.GetEnrollmentById(enrollmentId);
        if (enrollment == null)
        {
            throw new InvalidOperationException($"Enrollmentwith id:{enrollmentId} does not found.");
        }
        var enrollmentStatusList = (await _userEnrollmentRepository.GetEnrollmentStatuses()).Select(enrollmentStatus =>
        {
            return new SelectListItem { Value = enrollmentStatus.Id.ToString(), Text = enrollmentStatus.EnrollmentStatusName, Selected = enrollment.EnrollmentStatusId == enrollmentStatus.Id };
        });
        var data = new UpdateEnrollmentStatusModel
        {
            EnrollmentId= enrollmentId,
            EnrollmentStatusId = enrollment.EnrollmentStatusId,
            EnrollmentStatusList = enrollmentStatusList
        };
        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateEnrollmentStatus(UpdateEnrollmentStatusModel data)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                data.EnrollmentStatusList = (await _userEnrollmentRepository.GetEnrollmentStatuses()).Select(enrollmentStatus =>
                {
                    return new SelectListItem { Value = enrollmentStatus.Id.ToString(), Text = enrollmentStatus.EnrollmentStatusName, Selected = enrollmentStatus.Id == data.EnrollmentStatusId };
                });

                return View(data);
            }
            await _userEnrollmentRepository.ChangeEnrollmentStatus(data);
            TempData["msg"] = "Updated successfully";
        }
        catch (Exception ex)
        {
            // catch exception here
            TempData["msg"] = "Something went wrong";
        }
        return RedirectToAction(nameof(UpdateEnrollmentStatus), new { enrollmentId = data.EnrollmentId});
    }


    public IActionResult Dashboard()
    {
        return View();
    }

}
