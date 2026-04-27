using WaterMonitoringIOT.Models;

namespace WaterMonitoringIOT.Interfaces
{
    public interface UserInterface
    {
        public Task<(bool, string)> Login(string Username, string Password);
        public Task AddUser(string Name, string Username, string Password);
        public Task<SensorReadings> GetSensorReading(int DeviceId);
    }
}
