using WaterMonitoringIOT.Models;

namespace WaterMonitoringIOT.Interfaces
{
    public interface UserInterface
    {
        public Task<(bool, string)> Login(string Username, string Password);
        public Task AddUser(string Name, string Username, string Password);
        public Task<SensorReadings> GetSensorReading(int DeviceId);
        public Task<List<SensorReadingsLogs>> GetSensorReadingsLogs(int DeviceId);
        public Task<List<Devices>> GetDevices();
        public Task<List<SensorStatisticsDto>> GetSensorStatistics(int DeviceId,DateTime StartDate,DateTime EndDate);
        public Task ForceRead(int DeviceId);
         public Task ForceActuatorOn(int DeviceId);
        public Task ForceActuatorOff(int DeviceId);
    }
}
