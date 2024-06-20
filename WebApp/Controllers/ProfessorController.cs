using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(string courseName, string className)
        {
            var user = await _userManager.GetUserAsync(User);
            var course = new Course
            {
                Name = courseName,
                Class = className,
                ProfessorId = user.Id.ToString()
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult CreateQuestionnaire()
        {
            return RedirectToAction("Create", "Questionnaire");
        }
    }
}