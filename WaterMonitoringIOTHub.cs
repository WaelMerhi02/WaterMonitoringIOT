using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Data;
using WaterMonitoringIOT.Interfaces;
using WaterMonitoringIOT.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Connection = WaterMonitoringIOT.Models.Connection;

namespace WaterMonitoringIOT
{
    [Authorize]
    public class WaterMonitoringIOTHub:Hub
    {
        protected Info CurrentInfo
        {
            get
            {
                Info info = new Info();

                var claims = Context.User?.Claims.ToList();

                info.Id = Convert.ToInt32(
                    claims?.FirstOrDefault(c => c.Type == ClaimKeys.Id)?.Value
                );

                info.Username = Convert.ToString(
                    claims?.FirstOrDefault(c => c.Type == ClaimKeys.Username)?.Value
                );

                info.Type = Convert.ToString(
                    claims?.FirstOrDefault(c => c.Type == ClaimKeys.Type)?.Value
                );

                return info;
            }
        }

        private readonly DeviceInterface deviceInterface;

       
        public WaterMonitoringIOTHub(DeviceInterface _deviceInterface)
        {
            deviceInterface = _deviceInterface;
        }

        public override Task OnConnectedAsync()
        {
            if(CurrentInfo.Type==GlobalEnums.Type.Device.ToString())
                deviceInterface.UpdateDeviceStatus(CurrentInfo.Id,true);
            ConnectionMapping.Add(new Connection(Context.ConnectionId,CurrentInfo.Id,CurrentInfo.Type));

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? e)
        {
            if (CurrentInfo.Type == GlobalEnums.Type.Device.ToString())
                deviceInterface.UpdateDeviceStatus(CurrentInfo.Id, false);
            ConnectionMapping.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(e);
        }

 
    }
}
