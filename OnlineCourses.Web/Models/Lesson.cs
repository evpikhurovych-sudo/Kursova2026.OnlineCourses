namespace OnlineCourses.Web.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string TheoryText { get; set; } = "";

        public string TaskText { get; set; } = "";

        public string VideoUrl { get; set; } = "";

        public int CourseId { get; set; }

        public Course? Course { get; set; }
    }
}