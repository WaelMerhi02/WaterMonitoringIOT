using WaterMonitoringIOT.Models;

namespace WaterMonitoringIOT.Interfaces
{
    public interface DeviceInterface
    {
        public Task AddDevice(string DeviceName, string DeviceCode,string DevicePassword);
        public Task AddSensorReading(int DeviceId,decimal TemperatureValue,decimal PHValue,DateTime Date);
        public Task UpdateDeviceStatus(int DeviceId, bool IsActive);
        public Task<(bool, string)> VerifyDevice(string DeviceCode, string DevicePassword);
        public Task SendHeartBeat(int DeviceId,DateTime Date);
        Task<DeviceCommandsDTO> GetDeviceCommands(int deviceId);
    }
}
