using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

            var courses = _context.Courses
                .Where(c => c.Class == user.Class)
                .ToList();

            ViewBag.courses = courses;

            //var questionnaires = await _context.Questionnaires
            //    .Where(q => q.CourseId == CourseId)
            //    .ToListAsync();

            return View(courses);
        }
    }
}