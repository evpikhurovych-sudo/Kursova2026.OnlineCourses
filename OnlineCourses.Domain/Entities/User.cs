namespace OnlineCourses.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public UserRole Role { get; private set; }

        public User(string name, string email, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Invalid email");

            Name = name;
            Email = email;
            Role = role;
        }
    }
}
