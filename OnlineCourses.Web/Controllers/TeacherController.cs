using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourses.Web.Data;

namespace OnlineCourses.Web.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Teacher")
            {
                return RedirectToAction("Login", "Account");
            }

            var courses = await _context.Courses
                .Include(c => c.Lessons)
                .ToListAsync();

            return View(courses);
        }
    }
}