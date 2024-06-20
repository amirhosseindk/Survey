using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Student")]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;

        public HomeController(UserManager<User> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var courses = await _context.Courses
                .Where(c => c.Students.Any(s => s.Id == user.Id))
                .ToListAsync();

            var courseIds = courses.Select(c => c.Id).ToList();

            var questionnaires = await _context.Questionnaires
                .Where(q => courseIds.Contains(q.CourseId))
                .ToListAsync();

            return View(questionnaires);
        }
    }
}