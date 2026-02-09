using OnlineCourses.Domain.Entities;
using OnlineCourses.Domain.Interfaces;

namespace OnlineCourses.Domain.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        public Enrollment Enroll(Guid studentId, Guid courseId)
        {
            return new Enrollment(studentId, courseId);
        }

        public void UpdateProgress(Enrollment enrollment, int percent)
        {
            enrollment.UpdateProgress(percent);
        }
    }
}
