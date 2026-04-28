namespace WaterMonitoringIOT.Models
{
    public class Connection
    {
        public string ConnectionId { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }

        public Connection(string connectionId,int id,string type)
        {
            ConnectionId= connectionId;
            Id= id;
            Type= type;
        }
    }
}
