using WebApp.Models;

namespace WebApp.IServices
{
    public interface IStatisticalAnalysisService
    {
        Task<(double tStat, double pValue, double group1Mean, double group2Mean)> PerformPairedTTestAsync(int classId1, int classId2);
        Task<(double meanGroup1, double meanGroup2)> CalculateGroupMeansAsync(int classId1, int classId2);
        Task<(double overallMean, double deviation)> CompareWithCollegeAverageAsync(int classId, double collegeAverage);
        Task<(double tStatistic, double pValue, double classMean)> PerformOneSampleTTestAsync(int classId, double collegeAverage);
        Task<(double fStatistic, double pValue, bool isSignificant, double[] groupMeans)> PerformANOVAAsync(int[] classIds);
        Task<(double fStatistic, double pValue, bool isSignificant, double[] timePointMeans)> PerformRepeatedMeasuresANOVAAsync(int[] classIds, int timePoints);
    }
}