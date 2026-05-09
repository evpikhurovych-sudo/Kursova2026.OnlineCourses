namespace OnlineCourses.Web.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; } = "";

        public string Password { get; set; } = "";

        public string Role { get; set; } = "Student";

        public bool IsBlocked { get; set; } = false;

        public List<Enrollment> Enrollments { get; set; } = new();
    }
}