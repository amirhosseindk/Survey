using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.IServices;

namespace WebApp.Controllers
{
    public class StatisticalAnalysisController : Controller
    {
        private readonly IStatisticalAnalysisService _statisticalAnalysisService;
        private readonly AppDbContext _context;

        public StatisticalAnalysisController(IStatisticalAnalysisService statisticalAnalysisService, AppDbContext context)
        {
            _statisticalAnalysisService = statisticalAnalysisService;
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Professor")]
        public IActionResult PairedTTest()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> PairedTTest(int[] questionnaireIds)
        {
            if (questionnaireIds == null || questionnaireIds.Length != 2)
            {
                return BadRequest("Exactly two questionnaires must be selected.");
            }

            var courseId1 = await _context.Questionnaires
                .Where(q => q.Id == questionnaireIds[0])
                .Select(q => q.CourseId)
            .FirstOrDefaultAsync();

            var courseId2 = await _context.Questionnaires
                .Where(q => q.Id == questionnaireIds[1])
                .Select(q => q.CourseId)
                .FirstOrDefaultAsync();

            var (tStat, pValue) = await _statisticalAnalysisService.PerformPairedTTestAsync(courseId1, courseId2);
            ViewBag.TStat = tStat;
            ViewBag.PValue = pValue;
            return View();
        }
    }
}
