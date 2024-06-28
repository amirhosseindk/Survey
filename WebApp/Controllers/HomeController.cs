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

            var classes = await _context.Classes
                .Where(cl => cl.Students.Any(s => s.Id == user.Id))
                .Include(cl => cl.Course)
                .ToListAsync();

            var classIds = classes.Select(cl => cl.Id).ToList();

            var questionnaires = await _context.Questionnaires
                .Where(q => classIds.Contains(q.ClassId))
                .Include(q => q.Class)
                .ThenInclude(cl => cl.Course)
                .ToListAsync();

            return View(questionnaires);
        }
    }
}