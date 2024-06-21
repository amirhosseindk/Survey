﻿using WebApp.Models;

namespace WebApp.IServices
{
    public interface IStatisticalAnalysisService
    {
        Task<(double tStat, double pValue)> PerformPairedTTestAsync(int courseId1, int courseId2);
        Task<(double meanGroup1, double meanGroup2)> CalculateGroupMeansAsync(int courseId1, int courseId2);
        Task<(double overallMean, double deviation)> CompareWithCollegeAverageAsync(int courseId, double collegeAverage);
        Task<(double tStatistic, double pValue)> PerformOneSampleTTestAsync(int courseId, double collegeAverage);
        Task<AnovaResult> PerformANOVAAsync(int[] courseIds);
        Task<AnovaResult> PerformRepeatedMeasuresANOVAAsync(int[] courseIds, int timePoints);
    }
}