using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WaterMonitoringIOT.Interfaces;
using WaterMonitoringIOT.Models;

namespace WaterMonitoringIOT.Repo
{
    public class UserRepo:UserInterface
    {
        private readonly WaterMonitoringIOTDbContext context;

        public UserRepo(WaterMonitoringIOTDbContext _context)
        {
            context = _context;
        }
        public async Task<(bool, string)> Login(string Username, string Password)
        {
            Users? user = await context.Users.FirstOrDefaultAsync(d => d.Username == Username);
            if (user == null)
            {
                return (false, "User not found");
            }
            bool isPasswordValid = GlobalFunctions.VerifyPassword(user.Password, Password);
            if (!isPasswordValid)
            {
                return (false, "Invalid password");
            }

            string token = GlobalFunctions.GenerateToken(user.Id, user.Username, GlobalEnums.Type.User.ToString());
            return (true, token);
        }

        public async Task<SensorReadings> GetSensorReading(int DeviceId)
        {
            SensorReadings? sensor = await context.SensorReadings.FirstOrDefaultAsync(d => d.DeviceId == DeviceId);
            return sensor;
        }
        public async Task AddUser(string Name, string Username, string Password)
        {
            string HashedPassword = GlobalFunctions.HashPassword(Password);
            await context.Database.ExecuteSqlRawAsync(
             "EXEC AddUser @Name, @Username, @Password",
             new SqlParameter("@Name", Name),
             new SqlParameter("@Username", Username),
             new SqlParameter("@Password", HashedPassword));
        }

        public async Task<List<SensorReadingsLogs>> GetSensorReadingsLogs(int DeviceId)
        {
            List<SensorReadingsLogs>? logs = await context.SensorReadingsLogs.Where(x => x.DeviceId == DeviceId).ToListAsync();
            return logs;
        }

        public async Task<List<Devices>> GetDevices()
        {
            var device = await context.Devices.ToListAsync();
            return device;
        }
    }
}
