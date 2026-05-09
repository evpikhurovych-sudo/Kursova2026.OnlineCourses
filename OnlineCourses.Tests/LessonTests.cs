using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineCourses.Domain.Entities;

namespace OnlineCourses.Tests
{
    [TestClass]
    public class LessonTests
    {
        [TestMethod]
        public void Constructor_Should_Create_Lesson()
        {
            var lesson = new Lesson("Lesson 1", "Lesson content", 1);

            Assert.IsNotNull(lesson);
        }

        [TestMethod]
        public void Constructor_Should_Set_Title()
        {
            var lesson = new Lesson("Lesson 1", "Lesson content", 1);

            Assert.AreEqual("Lesson 1", lesson.Title);
        }

        [TestMethod]
        public void Constructor_Should_Set_Content()
        {
            var lesson = new Lesson("Lesson 1", "Lesson content", 1);

            Assert.AreEqual("Lesson content", lesson.Content);
        }
    }
}