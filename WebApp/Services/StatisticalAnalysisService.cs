using Microsoft.EntityFrameworkCore;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.Distributions;
using WebApp.Models;
using Accord.Statistics.Testing;
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

        public async Task<(double tStat, double pValue)> PerformPairedTTestAsync(int classId1, int classId2)
        {
            var answersGroup1 = await GetMultipleChoiceAnswersAsync(classId1);
            var answersGroup2 = await GetMultipleChoiceAnswersAsync(classId2);

            if (answersGroup1.Length != answersGroup2.Length)
                throw new InvalidOperationException("The number of responses must be equal for both groups.");

            return PerformPairedTTest(answersGroup1, answersGroup2);
        }

        public async Task<(double meanGroup1, double meanGroup2)> CalculateGroupMeansAsync(int classId1, int classId2)
        {
            var answersGroup1 = await GetMultipleChoiceAnswersAsync(classId1);
            var answersGroup2 = await GetMultipleChoiceAnswersAsync(classId2);

            return CalculateGroupMeans(answersGroup1, answersGroup2);
        }

        public async Task<(double overallMean, double deviation)> CompareWithCollegeAverageAsync(int classId, double collegeAverage)
        {
            var answers = await GetMultipleChoiceAnswersAsync(classId);

            return CompareWithCollegeAverage(answers, collegeAverage);
        }

        public async Task<(double tStatistic, double pValue)> PerformOneSampleTTestAsync(int classId, double collegeAverage)
        {
            var answers = await GetMultipleChoiceAnswersAsync(classId);

            return PerformOneSampleTTest(answers, collegeAverage);
        }

        public async Task<AnovaResult> PerformANOVAAsync(int[] classIds)
        {
            var responses = new double[classIds.Length][];
            for (int i = 0; i < classIds.Length; i++)
            {
                responses[i] = await GetMultipleChoiceAnswersAsync(classIds[i]);
            }

            return PerformANOVA(responses);
        }

        public async Task<AnovaResult> PerformRepeatedMeasuresANOVAAsync(int[] classIds, int timePoints)
        {
            var responses = new double[classIds.Length][];
            for (int i = 0; i < classIds.Length; i++)
            {
                responses[i] = await GetMultipleChoiceAnswersAsync(classIds[i]);
            }

            return PerformRepeatedMeasuresANOVA(responses, timePoints);
        }

        private async Task<double[]> GetMultipleChoiceAnswersAsync(int classId)
        {
            return await _context.Answers
                .Where(a => _context.Questionnaires
                                    .Where(q => q.ClassId == classId)
                                    .Select(q => q.Id)
                                    .Contains(a.QuestionnaireId) &&
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

        private (double, double) CalculateGroupMeans(double[] group1, double[] group2)
        {
            double meanGroup1 = group1.Average();
            double meanGroup2 = group2.Average();

            return (meanGroup1, meanGroup2);
        }

        private (double, double) CompareWithCollegeAverage(double[] group, double collegeAverage)
        {
            double total = group.Sum();
            double overallMean = total / group.Length;
            double deviation = overallMean - collegeAverage;

            return (overallMean, deviation);
        }

        private (double tStatistic, double pValue) PerformOneSampleTTest(double[] data, double populationMean)
        {
            int sampleSize = data.Length;
            double sampleMean = Statistics.Mean(data);
            double sampleStdDev = Statistics.StandardDeviation(data);

            double tStatistic = (sampleMean - populationMean) / (sampleStdDev / Math.Sqrt(sampleSize));
            int degreesOfFreedom = sampleSize - 1;

            var tDistribution = new StudentT(0, 1, degreesOfFreedom);
            double pValue = 2 * tDistribution.CumulativeDistribution(-Math.Abs(tStatistic)); // آزمون دو طرفه

            return (tStatistic, pValue);
        }

        private AnovaResult PerformANOVA(double[][] data)
        {
            int numGroups = data.Length;
            int totalResponses = data.Sum(d => d.Length);
            int[] groups = new int[totalResponses];
            int index = 0;

            for (int groupIndex = 0; groupIndex < numGroups; groupIndex++)
            {
                for (int i = 0; i < data[groupIndex].Length; i++)
                {
                    groups[index] = groupIndex + 1;
                    index++;
                }
            }

            double[] flatData = data.SelectMany(d => d).ToArray();
            OneWayAnova anova = new OneWayAnova(flatData, groups);

            bool isSignificant = anova.FTest.PValue < 0.05;

            return new AnovaResult
            {
                FStatistic = anova.FTest.Statistic,
                PValue = anova.FTest.PValue,
                IsSignificant = isSignificant
            };
        }

        private AnovaResult PerformRepeatedMeasuresANOVA(double[][] data, int timePoints)
        {
            int numGroups = data.Length;
            int numUsers = data[0].Length / timePoints;
            int totalResponses = numUsers * timePoints * numGroups;

            // فلت کردن داده‌ها برای ANOVA
            double[] flatData = new double[totalResponses];
            int index = 0;

            for (int i = 0; i < numGroups; i++)
            {
                for (int j = 0; j < numUsers * timePoints; j++)
                {
                    flatData[index] = data[i][j];
                    index++;
                }
            }

            // برچسب‌های زمانی
            int[] timeLabels = new int[totalResponses];
            index = 0;

            for (int t = 0; t < timePoints; t++)
            {
                for (int i = 0; i < numGroups * numUsers; i++)
                {
                    timeLabels[index] = t + 1;
                    index++;
                }
            }

            // انجام ANOVA
            OneWayAnova anova = new OneWayAnova(flatData, timeLabels);

            bool isSignificant = anova.FTest.PValue < 0.05;

            return new AnovaResult
            {
                FStatistic = anova.FTest.Statistic,
                PValue = anova.FTest.PValue,
                IsSignificant = isSignificant
            };
        }
    }
}