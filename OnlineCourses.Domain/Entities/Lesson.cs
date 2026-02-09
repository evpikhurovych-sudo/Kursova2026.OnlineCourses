namespace OnlineCourses.Domain.Entities
{
    public class Lesson : BaseEntity
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public int OrderNumber { get; private set; }

        public Lesson(string title, string content, int orderNumber)
        {
            Title = title;
            Content = content;
            OrderNumber = orderNumber;
        }
    }
}
