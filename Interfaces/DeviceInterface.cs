using WaterMonitoringIOT.Models;

namespace WaterMonitoringIOT.Interfaces
{
    public interface DeviceInterface
    {
        public Task AddDevice(string DeviceName, string DeviceCode,string DevicePassword);
        public Task AddSensorReading(int DeviceId,decimal TemperatureValue,decimal PHValue,DateTime Date);
        public Task<(bool, string)> VerifyDevice(string DeviceCode, string DevicePassword);
    }
}
