using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineCourses.Domain.Entities;
using System;

namespace OnlineCourses.Tests
{
    [TestClass]
    public class EnrollmentTests
    {
        [TestMethod]
        public void Constructor_Should_Set_StudentId()
        {
            var studentId = Guid.NewGuid();
            var courseId = Guid.NewGuid();

            var enrollment = new Enrollment(studentId, courseId);

            Assert.AreEqual(studentId, enrollment.StudentId);
        }

        [TestMethod]
        public void Constructor_Should_Set_CourseId()
        {
            var studentId = Guid.NewGuid();
            var courseId = Guid.NewGuid();

            var enrollment = new Enrollment(studentId, courseId);

            Assert.AreEqual(courseId, enrollment.CourseId);
        }

        [TestMethod]
        public void Constructor_Should_Set_Initial_Progress_To_Zero()
        {
            var enrollment = new Enrollment(Guid.NewGuid(), Guid.NewGuid());

            Assert.AreEqual(0, enrollment.ProgressPercent);
        }

        [TestMethod]
        public void Constructor_Should_Set_EnrolledAt()
        {
            var enrollment = new Enrollment(Guid.NewGuid(), Guid.NewGuid());

            Assert.AreNotEqual(default(DateTime), enrollment.EnrolledAt);
        }

        [TestMethod]
        public void UpdateProgress_Should_Set_Valid_Value()
        {
            var enrollment = new Enrollment(Guid.NewGuid(), Guid.NewGuid());

            enrollment.UpdateProgress(50);

            Assert.AreEqual(50, enrollment.ProgressPercent);
        }

        [TestMethod]
        public void UpdateProgress_Should_Throw_When_Value_Is_Invalid()
        {
            var enrollment = new Enrollment(Guid.NewGuid(), Guid.NewGuid());

            try
            {
                enrollment.UpdateProgress(101);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}