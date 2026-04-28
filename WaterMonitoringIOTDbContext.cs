using Microsoft.EntityFrameworkCore;
using WaterMonitoringIOT.Models;

namespace WaterMonitoringIOT
{
    public class WaterMonitoringIOTDbContext:DbContext
    {
        public WaterMonitoringIOTDbContext(DbContextOptions<WaterMonitoringIOTDbContext> options) : base(options)
        {
        }
        public DbSet<Devices> Devices { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<SensorReadings> SensorReadings { get; set; }
        public DbSet<SensorReadingsLogs> SensorReadingsLogs { get; set; }
    }
}
