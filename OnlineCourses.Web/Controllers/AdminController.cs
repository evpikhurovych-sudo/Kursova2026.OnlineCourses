using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourses.Web.Data;
using OnlineCourses.Web.Models;

namespace OnlineCourses.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("UserRole") == "Admin";
        }

        public async Task<IActionResult> Index()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            ViewBag.UsersCount = await _context.Users.CountAsync();
            ViewBag.CoursesCount = await _context.Courses.CountAsync();
            ViewBag.EnrollmentsCount = await _context.Enrollments.CountAsync();

            return View();
        }

        public async Task<IActionResult> Courses()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var courses = await _context.Courses
                .Include(c => c.Enrollments)
                .ToListAsync();

            return View(courses);
        }

        public IActionResult CreateCourse()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            if (string.IsNullOrWhiteSpace(course.Title) ||
                string.IsNullOrWhiteSpace(course.Description) ||
                string.IsNullOrWhiteSpace(course.Duration))
            {
                ViewBag.Error = "Необхідно заповнити всі обов’язкові поля.";
                return View(course);
            }

            course.ImageUrl = string.IsNullOrWhiteSpace(course.ImageUrl)
                ? "/images/programming.jpg"
                : course.ImageUrl;

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Курс успішно додано до системи.";
            return RedirectToAction("Courses");
        }

        public async Task<IActionResult> EditCourse(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var course = await _context.Courses
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return NotFound();

            if (course.Enrollments.Any())
            {
                TempData["Error"] = "Курс не може бути відредагований, оскільки на нього вже записані користувачі.";
                return RedirectToAction("Courses");
            }

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(Course course)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var hasUsers = await _context.Enrollments.AnyAsync(e => e.CourseId == course.Id);

            if (hasUsers)
            {
                TempData["Error"] = "Курс не може бути відредагований, оскільки на нього вже записані користувачі.";
                return RedirectToAction("Courses");
            }

            if (string.IsNullOrWhiteSpace(course.Title) ||
                string.IsNullOrWhiteSpace(course.Description) ||
                string.IsNullOrWhiteSpace(course.Duration))
            {
                ViewBag.Error = "Необхідно заповнити всі обов’язкові поля.";
                return View(course);
            }

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Дані курсу успішно оновлено.";
            return RedirectToAction("Courses");
        }

        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var hasUsers = await _context.Enrollments.AnyAsync(e => e.CourseId == id);

            if (hasUsers)
            {
                TempData["Error"] = "Курс не може бути видалений.";
                return RedirectToAction("Courses");
            }

            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Курс успішно видалено.";
            return RedirectToAction("Courses");
        }

        public async Task<IActionResult> Users()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(int id, string role)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var user = await _context.Users.FindAsync(id);

            if (user == null) return NotFound();

            user.Role = role;
            await _context.SaveChangesAsync();

            TempData["Message"] = "Роль користувача успішно змінено.";
            return RedirectToAction("Users");
        }

        public async Task<IActionResult> BlockUser(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var user = await _context.Users.FindAsync(id);

            if (user == null) return NotFound();

            user.IsBlocked = true;
            await _context.SaveChangesAsync();

            TempData["Message"] = "Користувач заблокований і не має доступу до системи.";
            return RedirectToAction("Users");
        }

        public async Task<IActionResult> UnblockUser(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var user = await _context.Users.FindAsync(id);

            if (user == null) return NotFound();

            user.IsBlocked = false;
            await _context.SaveChangesAsync();

            TempData["Message"] = "Користувача розблоковано.";
            return RedirectToAction("Users");
        }
    }
}