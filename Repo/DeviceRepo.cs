using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WaterMonitoringIOT.Interfaces;
using WaterMonitoringIOT.Models;

namespace WaterMonitoringIOT.Repo
{
    public class DeviceRepo:DeviceInterface
    {
        private readonly WaterMonitoringIOTDbContext context;

        public DeviceRepo(WaterMonitoringIOTDbContext _context)
        {
            context = _context;
        }

        public async Task<(bool, string)> VerifyDevice(string DeviceCode, string DevicePassword) { 
            Devices? device = await context.Devices.FirstOrDefaultAsync(d => d.DeviceCode == DeviceCode);
            if (device == null)
            {
                return (false, "Device not found");
            }
            bool isPasswordValid = GlobalFunctions.VerifyPassword(device.DevicePassword, DevicePassword);
            if (!isPasswordValid)
            {
                return (false, "Invalid password");
            }

            string token = GlobalFunctions.GenerateToken(device.Id, device.DeviceCode,GlobalEnums.Type.Device.ToString());
            return (true, token);
        }

        public async Task UpdateDeviceStatus(int deviceId, bool isActive)
        {
            if (!isActive)
            {
                    await context.Database.ExecuteSqlRawAsync(
                 "EXEC TurnOffDeviceStatus @DeviceId",
                new SqlParameter("@DeviceId", deviceId)
                 );
                    await context.SaveChangesAsync();

            }
            else
            {
                await context.Database.ExecuteSqlRawAsync(
                 "EXEC TurnOnDeviceStatus @DeviceId",
                new SqlParameter("@DeviceId", deviceId)
                 );
                await context.SaveChangesAsync();
            }
          
        }


        public async Task AddDevice(string DeviceName, string DeviceCode, string DevicePassword)
        {
            string HashedPassword= GlobalFunctions.HashPassword(DevicePassword);
            await context.Database.ExecuteSqlRawAsync(
             "EXEC AddDevice @DeviceName, @DeviceCode, @DevicePassword",
             new SqlParameter("@DeviceName", DeviceName),
             new SqlParameter("@DeviceCode", DeviceCode),
             new SqlParameter("@DevicePassword", HashedPassword));
        }

        public async Task AddSensorReading(int DeviceId, decimal TemperatureValue, decimal PHValue, DateTime Date)
        {
            await context.Database.ExecuteSqlRawAsync(
             "EXEC AddSensorReading @DeviceId, @TemperatureValue, @PHValue,@Date",
             new SqlParameter("@DeviceId", DeviceId),
             new SqlParameter("@TemperatureValue", TemperatureValue),
             new SqlParameter("@PHValue", PHValue),
             new SqlParameter("@Date", Date));
        }

       
    }
}
