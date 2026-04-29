using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WaterMonitoringIOT.Interfaces;

namespace WaterMonitoringIOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserInterface userInterface;

        public UserController(UserInterface _userInterface)
        {
            userInterface = _userInterface;
        }

        [Route("Test")]
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("WEBSITE DEPLOYED");
        }

        [Route("Login")]
        [HttpGet]
        public async Task<IActionResult> Login(string Username, string Password)
        {
            (bool, string) IsUserVerified = await userInterface.Login(Username, Password);
            if (IsUserVerified.Item1)
            {
                return Ok(IsUserVerified.Item2);
            }
            return Unauthorized(new { Message = IsUserVerified.Item2 });
        }

        [Route("AddUser")] 
        [HttpPost]
        public async Task<IActionResult> AddUser(string Name, string Username, string Password)
        {
            await userInterface.AddUser(Name, Username, Password);
            return Ok("User Added");
        }

        [HttpGet]
        [Authorize(Policy = "UserOnly")]
        [Route("GetSensorReading")]
        public async Task<IActionResult> GetSensorReading(int DeviceId)
        {
            return Ok(await userInterface.GetSensorReading(DeviceId));
        }

        [HttpGet]
        [Authorize(Policy = "UserOnly")]
        [Route("GetSensorReadingLogs")]
        public async Task<IActionResult> GetSensorReadingLogs(int DeviceId)
        {
            return Ok(await userInterface.GetSensorReadingsLogs(DeviceId));
        }

        [HttpGet]
        [Authorize(Policy = "UserOnly")]
        [Route("GetDevices")]
        public async Task<IActionResult> GetDevices()
        {
            return Ok(await userInterface.GetDevices());
        }

        [HttpGet]
        [Authorize(Policy = "UserOnly")]
        [Route("GetSensorStatistics")]
        public async Task<IActionResult> GetSensorStatistics(int DeviceId,DateTime StartDate,DateTime EndDate)
        {
            return Ok(await userInterface.GetSensorStatistics(DeviceId, StartDate, EndDate));
        }

        [HttpPost]
        [Authorize(Policy = "UserOnly")]
        [Route("ForceRead")]
        public async Task<IActionResult> ForceRead(int DeviceId)
        {
            await userInterface.ForceRead(DeviceId);
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "UserOnly")]
        [Route("ForceActuatorOn")]
        public async Task<IActionResult> ForceActuatorOn(int DeviceId)
        {
            await userInterface.ForceActuatorOn(DeviceId);
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "UserOnly")]
        [Route("ForceActuatorOff")]
        public async Task<IActionResult> ForceActuatorOff(int DeviceId)
        {
            await userInterface.ForceActuatorOff(DeviceId);
            return Ok();
        }



    }
}
