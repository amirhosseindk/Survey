namespace WebApp.IServices
{
    public interface IStatisticalAnalysisService
    {
        Task<(double tStat, double pValue)> PerformPairedTTestAsync(int courseId1, int courseId2);
    }
}