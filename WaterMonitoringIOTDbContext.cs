using Microsoft.EntityFrameworkCore;
using WaterMonitoringIOT.Models;

namespace WaterMonitoringIOT
{
    public class WaterMonitoringIOTDbContext:DbContext
    {
        public WaterMonitoringIOTDbContext(DbContextOptions<WaterMonitoringIOTDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SensorStatisticsDto>().HasNoKey();
        }
        public DbSet<Devices> Devices { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<SensorReadings> SensorReadings { get; set; }
        public DbSet<SensorReadingsLogs> SensorReadingsLogs { get; set; }
        public DbSet<SensorStatisticsDto> SensorStatistics { get; set; }
    }
}
