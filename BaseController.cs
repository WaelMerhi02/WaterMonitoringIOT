using Microsoft.AspNetCore.Mvc;
using WaterMonitoringIOT.Models;

namespace WaterMonitoringIOT
{
    public class BaseController:ControllerBase
    {
        protected Info CurrentInfo
        {
            get
            {
                Info Info = new Info();
                var claims = HttpContext.User.Claims.ToList();
                Info.Id = Convert.ToInt32(claims.FirstOrDefault(c => c.Type == ClaimKeys.Id)?.Value);
                Info.Username = Convert.ToString(claims.FirstOrDefault(c => c.Type == ClaimKeys.Username)?.Value);
                return Info;
            }

        }
    }
}
