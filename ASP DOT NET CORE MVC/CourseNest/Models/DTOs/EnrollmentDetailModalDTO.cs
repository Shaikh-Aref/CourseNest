namespace CourseNest.Models.DTOs;

public class EnrollmentDetailModalDTO
{
    public string DivId { get; set; }
    public IEnumerable<EnrollmentDetail> EnrollmentDetail { get; set; }
}
