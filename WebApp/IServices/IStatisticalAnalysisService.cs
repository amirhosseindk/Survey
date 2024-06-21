namespace WebApp.IServices
{
    public interface IStatisticalAnalysisService
    {
        Task<(double tStat, double pValue)> PerformPairedTTestAsync(int courseId1, int courseId2);
        Task<(double meanGroup1, double meanGroup2)> CalculateGroupMeansAsync(int courseId1, int courseId2);
        Task<(double overallMean, double deviation)> CompareWithCollegeAverageAsync(int courseId, double collegeAverage);
    }
}