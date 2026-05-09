namespace OnlineCourses.Web.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Duration { get; set; } = "";
        public string ImageUrl { get; set; } = "";

        public string TeacherName { get; set; } = "";
        public string TeacherPosition { get; set; } = "";
        public string TeacherDescription { get; set; } = "";
        public string TeacherPhotoUrl { get; set; } = "";
        public int TeacherExperienceYears { get; set; }
        public int TeacherReviewsCount { get; set; }
        public double TeacherRating { get; set; }

        public List<Lesson> Lessons { get; set; } = new();
        public List<Enrollment> Enrollments { get; set; } = new();
    }
}