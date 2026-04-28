namespace WaterMonitoringIOT.Models
{
    public static class ConnectionMapping
    {
        private static readonly List<Connection> Connections = new List<Connection>();

        public static void Add(Connection connection)
        {
            Connections.Add(connection);
        }

        public static void Remove(string connectionId)
        {
            Connections.RemoveAll(x => x.ConnectionId.Equals(connectionId));
        }

        public static Connection GetConnection(string connectionId)
        {
            return Connections.FirstOrDefault(x => x.ConnectionId.Equals(connectionId));
        }
    }
}
