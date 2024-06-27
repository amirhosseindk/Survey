using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Professor")]
    public class ProfessorController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;

        public ProfessorController(UserManager<User> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var courses = await _context.Courses
                .Where(c => c.ProfessorId == user.Id)
                .Include(c => c.Classes)
                .ThenInclude(cl => cl.Questionnaires)
                .ToListAsync();

            var questionnaires = courses.SelectMany(c => c.Classes)
                                        .SelectMany(cl => cl.Questionnaires)
                                        .ToList();

            ViewBag.Courses = courses;
            return View(questionnaires);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(string courseName)
        {
            var user = await _userManager.GetUserAsync(User);
            var course = new Course { Name = courseName, ProfessorId = user.Id.ToString() };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult CreateClass()
        {
            var userId = _userManager.GetUserId(User);
            var courses = _context.Courses
                .Where(c => c.ProfessorId == userId)
                .ToList();
            ViewBag.Courses = courses;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateClass(string className, int courseId)
        {
            var classEntity = new Class { Name = className, CourseId = courseId };
            _context.Classes.Add(classEntity);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult CreateQuestionnaire()
        {
            var userId = _userManager.GetUserId(User);
            var courses = _context.Courses
                .Where(c => c.ProfessorId == userId)
                .Include(c => c.Classes)
                .ToList();
            ViewBag.Courses = courses;
            return View();
        }
    }
}