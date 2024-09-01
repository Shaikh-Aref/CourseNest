using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CourseNest.Models.DTOs;

public class UpdateEnrollmentStatusModel
{
    public int EnrollmentId{ get; set; }

    [Required]
    public int EnrollmentStatusId { get; set; }

    public IEnumerable<SelectListItem>? EnrollmentStatusList { get; set; }
}
