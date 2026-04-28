using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WaterMonitoringIOT.Interfaces;

namespace WaterMonitoringIOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : BaseController
    {
        private readonly DeviceInterface deviceInterface;

        public DeviceController(DeviceInterface _deviceInterface)
        {
            deviceInterface = _deviceInterface;
        }

        [Route("AddSensorReading")]
        [Authorize(Policy = "DeviceOnly")]
        [HttpPost]
        public async Task<IActionResult> AddSensorReading(decimal TemperatureValue, decimal PHValue,DateTime Date)
        {
            await deviceInterface.AddSensorReading(CurrentInfo.Id, TemperatureValue, PHValue,Date);
            return Ok("Reading Added");
        }

        [Route("VerifyDevice")]
        [HttpGet]
        public async Task<IActionResult> VerifyDevice(string DeviceCode, string DevicePassword)
        {
            (bool, string) IsDeviceVerified = await deviceInterface.VerifyDevice(DeviceCode, DevicePassword);
            if (IsDeviceVerified.Item1)
            {
                return Ok(IsDeviceVerified.Item2);
            }
            return Unauthorized(new { Message = IsDeviceVerified.Item2 });
        }

        [Route("AddDevice")]
        [HttpPost]
        public async Task<IActionResult> AddDevice(string DeviceName,string DeviceCode,string DevicePassword)
        {
            await deviceInterface.AddDevice(DeviceName, DeviceCode, DevicePassword);
            return Ok("Device Added");
        }


       
    }
}