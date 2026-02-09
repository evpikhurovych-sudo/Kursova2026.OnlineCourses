using OnlineCourses.Domain.Events;

namespace OnlineCourses.Domain.Entities
{
    public class Enrollment : BaseEntity
    {
        public Guid StudentId { get; private set; }
        public Guid CourseId { get; private set; }
        public DateTime EnrolledAt { get; private set; }
        public int ProgressPercent { get; private set; }

        public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;

        public Enrollment(Guid studentId, Guid courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
            EnrolledAt = DateTime.UtcNow;
            ProgressPercent = 0;
        }

        public void UpdateProgress(int percent)
        {
            ProgressPercent = percent;

            // Raise event
            ProgressChanged?.Invoke(
                this,
                new ProgressChangedEventArgs(Id, ProgressPercent)
            );
        }
    }
}
