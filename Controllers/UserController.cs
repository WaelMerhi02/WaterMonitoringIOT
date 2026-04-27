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
                return Ok(new { Token = IsUserVerified.Item2 });
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

    }
}
