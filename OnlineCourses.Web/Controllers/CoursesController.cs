using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourses.Web.Data;
using OnlineCourses.Web.Models;

namespace OnlineCourses.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();
            return View(courses);
        }

        public async Task<IActionResult> Details(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Lessons)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return NotFound();

            return View(course);
        }

        public async Task<IActionResult> Enroll(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var alreadyEnrolled = await _context.Enrollments
                .AnyAsync(e => e.CourseId == id && e.UserId == userId.Value);

            if (!alreadyEnrolled)
            {
                _context.Enrollments.Add(new Enrollment
                {
                    CourseId = id,
                    UserId = userId.Value,
                    Progress = 0,
                    TestScore = 0
                });

                await _context.SaveChangesAsync();
                TempData["Message"] = "Ви успішно записалися на курс.";
            }
            else
            {
                TempData["Message"] = "Ви вже записані на цей курс.";
            }

            return RedirectToAction("Cabinet");
        }

        public async Task<IActionResult> Cabinet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var enrollments = await _context.Enrollments
                .Include(e => e.Course)
                .ThenInclude(c => c!.Lessons)
                .Where(e => e.UserId == userId.Value)
                .ToListAsync();

            return View(enrollments);
        }

        public async Task<IActionResult> Learn(int id, int lessonIndex = 0)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .ThenInclude(c => c!.Lessons)
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId.Value);

            if (enrollment == null) return NotFound();

            var lessonCount = enrollment.Course?.Lessons.Count ?? 0;

            if (lessonIndex < 0) lessonIndex = 0;
            if (lessonCount > 0 && lessonIndex >= lessonCount) lessonIndex = lessonCount - 1;

            ViewBag.LessonIndex = lessonIndex;

            return View(enrollment);
        }

        public async Task<IActionResult> CompleteLesson(int id, int lessonIndex)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .ThenInclude(c => c!.Lessons)
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId.Value);

            if (enrollment == null) return NotFound();

            var lessonCount = enrollment.Course?.Lessons.Count ?? 1;
            var lessonProgress = 80 / lessonCount;

            if (enrollment.Progress < 80)
            {
                enrollment.Progress += lessonProgress;

                if (enrollment.Progress > 80)
                    enrollment.Progress = 80;

                await _context.SaveChangesAsync();
            }

            var nextLesson = lessonIndex + 1;

            if (nextLesson >= lessonCount)
                return RedirectToAction("Test", new { id = enrollment.Id });

            return RedirectToAction("Learn", new { id = enrollment.Id, lessonIndex = nextLesson });
        }

        public async Task<IActionResult> Test(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId.Value);

            if (enrollment == null) return NotFound();

            return View(enrollment);
        }

        [HttpPost]

        public async Task<IActionResult> Teacher(int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        public async Task<IActionResult> SubmitTest(int id, string q1, string q2, string q3)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId.Value);

            if (enrollment == null) return NotFound();

            int score = 0;
            string title = enrollment.Course?.Title ?? "";

            if (title.Contains("програмування"))
            {
                if (q1 == "algorithm") score++;
                if (q2 == "variable") score++;
                if (q3 == "if") score++;
            }
            else if (title.Contains("Веб"))
            {
                if (q1 == "html") score++;
                if (q2 == "css") score++;
                if (q3 == "mvc") score++;
            }
            else if (title.Contains("Бази"))
            {
                if (q1 == "database") score++;
                if (q2 == "sql") score++;
                if (q3 == "relation") score++;
            }
            else if (title.Contains("UI"))
            {
                if (q1 == "uiux") score++;
                if (q2 == "layout") score++;
                if (q3 == "figma") score++;
            }

            enrollment.TestScore = score;

            if (score >= 2)
            {
                enrollment.Progress = 100;
            }

            await _context.SaveChangesAsync();

            ViewBag.Score = score;
            ViewBag.IsSubmitted = true;

            return View("Test", enrollment);
        }
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("Index");
            }

            var courses = await _context.Courses
                .Where(c => c.Title.Contains(query) || c.Description.Contains(query))
                .ToListAsync();

            ViewBag.Query = query;

            return View("Index", courses);
        }
    }
}