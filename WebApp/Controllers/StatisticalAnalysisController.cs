﻿using Microsoft.AspNetCore.Authorization;
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

            var courseId1 = await _context.Questionnaires
                .Where(q => q.Id == questionnaireIds[0])
                .Select(q => q.CourseId)
            .FirstOrDefaultAsync();

            var courseId2 = await _context.Questionnaires
                .Where(q => q.Id == questionnaireIds[1])
                .Select(q => q.CourseId)
                .FirstOrDefaultAsync();

            var (meanGroup1, meanGroup2) = await _statisticalAnalysisService.CalculateGroupMeansAsync(courseId1, courseId2);
            ViewBag.MeanGroup1 = meanGroup1;
            ViewBag.MeanGroup2 = meanGroup2;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> CompareWithCollegeAverage(int questionnaireId, double collegeAverage=2.4)
        {
            var courseId = await _context.Questionnaires
                .Where(q => q.Id == questionnaireId)
                .Select(q => q.CourseId)
                .FirstOrDefaultAsync();

            var (overallMean, deviation) = await _statisticalAnalysisService.CompareWithCollegeAverageAsync(courseId, collegeAverage);
            ViewBag.OverallMean = overallMean;
            ViewBag.Deviation = deviation;
            return View();
        }
    }
}