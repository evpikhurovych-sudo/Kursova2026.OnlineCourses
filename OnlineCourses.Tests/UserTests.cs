using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineCourses.Domain.Entities;

namespace OnlineCourses.Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void Constructor_Should_Create_User()
        {
            var user = new User("Olena", "olena@mail.com", UserRole.Student);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void Constructor_Should_Set_Name()
        {
            var user = new User("Olena", "olena@mail.com", UserRole.Student);

            Assert.AreEqual("Olena", user.Name);
        }

        [TestMethod]
        public void Constructor_Should_Set_Email()
        {
            var user = new User("Olena", "olena@mail.com", UserRole.Student);

            Assert.AreEqual("olena@mail.com", user.Email);
        }

        [TestMethod]
        public void Constructor_Should_Set_Role()
        {
            var user = new User("Olena", "olena@mail.com", UserRole.Student);

            Assert.AreEqual(UserRole.Student, user.Role);
        }

        [TestMethod]
        public void Constructor_Should_Assign_Id()
        {
            var user = new User("Olena", "olena@mail.com", UserRole.Student);

            Assert.AreNotEqual(Guid.Empty, user.Id);
        }
    }
}