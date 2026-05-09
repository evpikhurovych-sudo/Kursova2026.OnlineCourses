namespace OnlineCourses.Web.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }

        public int Progress { get; set; }

        public int TestScore { get; set; }
    }
}