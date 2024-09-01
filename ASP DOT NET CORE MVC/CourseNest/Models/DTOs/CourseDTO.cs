using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CourseNest.Models.DTOs;
public class CourseDTO
{
    public int Id { get; set; }

    [Required]
    [MaxLength(40)]
    public string? CourseName { get; set; }

    [Required]
    [MaxLength(40)]
    public string? InstructorName { get; set; }
    [Required]
    public double CourseFee { get; set; }
    public string? Image { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public IFormFile? ImageFile { get; set; }
    public IEnumerable<SelectListItem>? CategoryList { get; set; }
}
