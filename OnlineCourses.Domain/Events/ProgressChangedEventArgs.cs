namespace OnlineCourses.Domain.Events
{
    public class ProgressChangedEventArgs : EventArgs
    {
        public Guid EnrollmentId { get; }
        public int NewProgressPercent { get; }

        public ProgressChangedEventArgs(Guid enrollmentId, int newProgressPercent)
        {
            EnrollmentId = enrollmentId;
            NewProgressPercent = newProgressPercent;
        }
    }
}
