using Microsoft.AspNetCore.Mvc;
using OnlineCourses.Web.Data;

namespace OnlineCourses.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var courses = _context.Courses.Take(3).ToList();
            return View(courses);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}