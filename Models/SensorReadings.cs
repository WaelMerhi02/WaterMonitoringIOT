namespace WaterMonitoringIOT.Models
{
    public class SensorReadings
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public decimal TemperatureValue { get; set; }
        public decimal PHValue { get; set; }
        public DateTime Date { get; set; }
        public bool IsSafe { get; set; }


    }
}
