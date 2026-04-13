using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineCourses.Domain.Entities;
using System;

namespace OnlineCourses.Tests
{
    [TestClass]
    public class CourseTests
    {
        [TestMethod]
        public void Constructor_Should_Create_Course()
        {
            var authorId = Guid.NewGuid();
            var course = new Course("C# Basics", "Intro course", 1000m, authorId);

            Assert.IsNotNull(course);
        }

        [TestMethod]
        public void Constructor_Should_Set_Title()
        {
            var authorId = Guid.NewGuid();
            var course = new Course("C# Basics", "Intro course", 1000m, authorId);

            Assert.AreEqual("C# Basics", course.Title);
        }

        [TestMethod]
        public void Constructor_Should_Set_Description()
        {
            var authorId = Guid.NewGuid();
            var course = new Course("C# Basics", "Intro course", 1000m, authorId);

            Assert.AreEqual("Intro course", course.Description);
        }

        [TestMethod]
        public void Constructor_Should_Set_Price()
        {
            var authorId = Guid.NewGuid();
            var course = new Course("C# Basics", "Intro course", 1000m, authorId);

            Assert.AreEqual(1000m, course.Price);
        }

        [TestMethod]
        public void Constructor_Should_Set_Id()
        {
            var authorId = Guid.NewGuid();
            var course = new Course("C# Basics", "Intro course", 1000m, authorId);

            Assert.AreNotEqual(Guid.Empty, course.Id);
        }
    }
}