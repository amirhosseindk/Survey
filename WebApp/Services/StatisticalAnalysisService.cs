using Microsoft.EntityFrameworkCore;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.Distributions;
using WebApp.IServices;

namespace WebApp.Services
{
    public class StatisticalAnalysisService : IStatisticalAnalysisService
    {
        private readonly AppDbContext _context;

        public StatisticalAnalysisService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(double tStat, double pValue)> PerformPairedTTestAsync(int courseId1, int courseId2)
        {
            var answersGroup1 = await GetMultipleChoiceAnswersAsync(courseId1);
            var answersGroup2 = await GetMultipleChoiceAnswersAsync(courseId2);

            if (answersGroup1.Length != answersGroup2.Length)
                throw new InvalidOperationException("The number of responses must be equal for both groups.");

            return PerformPairedTTest(answersGroup1, answersGroup2);
        }

        private async Task<double[]> GetMultipleChoiceAnswersAsync(int courseId)
        {
            return await _context.Answers
                .Where(a => a.QuestionnaireId == _context.Questionnaires
                                               .Where(q => q.CourseId == courseId)
                                               .Select(q => q.Id)
                                               .FirstOrDefault() &&
                            a.AnswerOptionId.HasValue &&
                            string.IsNullOrEmpty(a.AnswerText))
                .Select(a => (double)a.AnswerOptionId.Value)
                .ToArrayAsync();
        }

        private (double, double) PerformPairedTTest(double[] group1, double[] group2)
        {
            int numUsers = group1.Length;

            double[] differences = new double[numUsers];
            for (int i = 0; i < numUsers; i++)
            {
                differences[i] = group1[i] - group2[i];
            }

            double meanDifference = Statistics.Mean(differences);
            double stdDev = Statistics.StandardDeviation(differences);

            int degreesOfFreedom = numUsers - 1;
            double tStat = meanDifference / (stdDev / Math.Sqrt(numUsers));
            var tDistribution = new StudentT(0, 1, degreesOfFreedom);

            double pValue = 2 * tDistribution.CumulativeDistribution(-Math.Abs(tStat)); // Two-tailed test

            return (tStat, pValue);
        }
    }
}