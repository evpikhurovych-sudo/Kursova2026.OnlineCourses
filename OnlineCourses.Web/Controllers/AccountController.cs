using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Web.Data;
using OnlineCourses.Web.Models;

namespace OnlineCourses.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string login, string password)
        {
            if (_context.Users.Any(u => u.Login == login))
            {
                ViewBag.Error = "Користувач з таким логіном вже існує.";
                return View();
            }

            var user = new User
            {
                Login = login,
                Password = password,
                Role = "Student"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserLogin", user.Login);
            HttpContext.Session.SetString("UserRole", user.Role);

            return RedirectToAction("Index", "Courses");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Неправильний логін або пароль.";
                return View();
            }
            if (user.IsBlocked)
            {
                ViewBag.Error = "Користувач заблокований і не має доступу до системи.";
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserLogin", user.Login);

            return RedirectToAction("Index", "Courses");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}