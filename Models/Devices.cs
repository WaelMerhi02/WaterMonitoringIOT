namespace WaterMonitoringIOT.Models
{
    public class Devices
    {
        public int Id { get; set; }
        public string DeviceCode { get; set; }
        public string DevicePassword { get; set; }
        public string DeviceName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastSeenAt { get; set; }
        public DateTime CreatedDate { get; set; }

        public Devices()
        {
        }
    }
}
