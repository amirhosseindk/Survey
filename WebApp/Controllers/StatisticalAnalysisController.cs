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

            var classId1 = await _context.Questionnaires
                .Where(q => q.Id == questionnaireIds[0])
                .Select(q => q.ClassId)
                .FirstOrDefaultAsync();

            var classId2 = await _context.Questionnaires
                .Where(q => q.Id == questionnaireIds[1])
                .Select(q => q.ClassId)
                .FirstOrDefaultAsync();

            var (tStat, pValue) = await _statisticalAnalysisService.PerformPairedTTestAsync(classId1, classId2);
            ViewBag.TStat = tStat;
            ViewBag.PValue = pValue;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Professor")]
        public IActionResult CalculateGroupMeans()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> CalculateGroupMeans(int[] questionnaireIds)
        {
            if (questionnaireIds == null || questionnaireIds.Length != 2)
            {
                return BadRequest("Exactly two questionnaires must be selected.");
            }

            var classId1 = await _context.Questionnaires
                .Where(q => q.Id == questionnaireIds[0])
                .Select(q => q.ClassId)
                .FirstOrDefaultAsync();

            var classId2 = await _context.Questionnaires
                .Where(q => q.Id == questionnaireIds[1])
                .Select(q => q.ClassId)
                .FirstOrDefaultAsync();

            var (meanGroup1, meanGroup2) = await _statisticalAnalysisService.CalculateGroupMeansAsync(classId1, classId2);
            ViewBag.MeanGroup1 = meanGroup1;
            ViewBag.MeanGroup2 = meanGroup2;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> CompareWithCollegeAverage(int questionnaireId, double collegeAverage = 2.4)
        {
            var classId = await _context.Questionnaires
                .Where(q => q.Id == questionnaireId)
                .Select(q => q.ClassId)
                .FirstOrDefaultAsync();

            var (overallMean, deviation) = await _statisticalAnalysisService.CompareWithCollegeAverageAsync(classId, collegeAverage);
            ViewBag.OverallMean = overallMean;
            ViewBag.Deviation = deviation;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> PerformOneSampleTTest(int questionnaireId, double collegeAverage = 2.4)
        {
            var classId = await _context.Questionnaires
                .Where(q => q.Id == questionnaireId)
                .Select(q => q.ClassId)
                .FirstOrDefaultAsync();

            var (tStatistic, pValue) = await _statisticalAnalysisService.PerformOneSampleTTestAsync(classId, collegeAverage);
            ViewBag.TStatistic = tStatistic;
            ViewBag.PValue = pValue;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Professor")]
        public IActionResult PerformANOVA()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> PerformANOVA(int[] questionnaireIds)
        {
            if (questionnaireIds == null || questionnaireIds.Length < 3)
            {
                return BadRequest("At least three questionnaires must be selected.");
            }

            var classIds = await _context.Questionnaires
                .Where(q => questionnaireIds.Contains(q.Id))
                .Select(q => q.ClassId)
                .ToArrayAsync();

            var result = await _statisticalAnalysisService.PerformANOVAAsync(classIds);
            ViewBag.FStatistic = result.FStatistic;
            ViewBag.PValue = result.PValue;
            ViewBag.IsSignificant = result.IsSignificant;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Professor")]
        public IActionResult PerformRepeatedMeasuresANOVA()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> PerformRepeatedMeasuresANOVA(int[] questionnaireIds)
        {
            if (questionnaireIds == null || questionnaireIds.Length < 3)
            {
                return BadRequest("At least three questionnaires must be selected.");
            }

            var classIds = await _context.Questionnaires
                .Where(q => questionnaireIds.Contains(q.Id))
                .Select(q => q.ClassId)
                .ToArrayAsync();

            var result = await _statisticalAnalysisService.PerformRepeatedMeasuresANOVAAsync(classIds, questionnaireIds.Length);
            ViewBag.FStatistic = result.FStatistic;
            ViewBag.PValue = result.PValue;
            ViewBag.IsSignificant = result.IsSignificant;
            return View();
        }
    }
}