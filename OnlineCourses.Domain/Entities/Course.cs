namespace OnlineCourses.Domain.Entities
{
    public class Course : BaseEntity
    {
        private readonly List<Lesson> _lessons = new();

        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public Guid InstructorId { get; private set; }

        public IReadOnlyCollection<Lesson> Lessons => _lessons.AsReadOnly();

        public Course(string title, string description, decimal price, Guid instructorId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative");

            Title = title;
            Description = description;
            Price = price;
            InstructorId = instructorId;
        }

        public void AddLesson(Lesson lesson)
        {
            if (lesson == null)
                throw new ArgumentNullException(nameof(lesson));

            _lessons.Add(lesson);
        }
    }
}
