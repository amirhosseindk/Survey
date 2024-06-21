namespace WebApp.Models
{
    public class AnovaResult
    {
        public double FStatistic { get; set; }
        public double PValue { get; set; }
        public bool IsSignificant { get; set; }
    }
}