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
            var questionnaires = await _context.Questionnaires
                .Where(q => q.ProfessorId == user.Id)
                .ToListAsync();

            return View(questionnaires);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(string courseName, string className)
        {
            var user = await _userManager.GetUserAsync(User);
            var course = new Course
            {
                Name = courseName + "-" + className,
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