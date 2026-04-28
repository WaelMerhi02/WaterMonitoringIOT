namespace WaterMonitoringIOT.Models
{
    public class SensorStatisticsDto
    {
        public int TotalReadings { get; set; }
        public decimal? AvgPH { get; set; }
        public decimal? MinPH { get; set; }
        public decimal? MaxPH { get; set; }
        public decimal? AvgTemperature { get; set; }
        public decimal? MinTemperature { get; set; }
        public decimal? MaxTemperature { get; set; }
        public int UnsafeReadings { get; set; }
    }
}
