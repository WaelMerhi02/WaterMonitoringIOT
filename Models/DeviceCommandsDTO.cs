namespace WaterMonitoringIOT.Models
{
    public class DeviceCommandsDTO
    {
        public bool ForceRead { get; set; }
        public bool ForceActuatorOn { get; set; }
        public bool ForceActuatorOff { get; set; }
    }
}
