namespace CourseNest.Repositories;

public interface IUserEnrollmentRepository
{
    Task<IEnumerable<Enrollment>> UserEnrollments(bool getAll=false);
    Task ChangeEnrollmentStatus(UpdateEnrollmentStatusModel data);
    Task TogglePaymentStatus(int enrollmentId);
    Task<Enrollment?> GetEnrollmentById(int id);
    Task<IEnumerable<EnrollmentStatus>> GetEnrollmentStatuses();

}