using System.Collections.ObjectModel;

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
            Title = title;
            Description = description;
            Price = price;
            InstructorId = instructorId;
        }

        public void AddLesson(Lesson lesson)
        {
            // заглушка (поки що без перевірок)
            _lessons.Add(lesson);
        }
    }
}
