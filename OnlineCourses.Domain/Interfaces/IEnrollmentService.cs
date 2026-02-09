using OnlineCourses.Domain.Entities;

namespace OnlineCourses.Domain.Interfaces
{
    public interface IEnrollmentService
    {
        Enrollment Enroll(Guid studentId, Guid courseId);
        void UpdateProgress(Enrollment enrollment, int percent);
    }
}
