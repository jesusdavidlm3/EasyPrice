using Libs;
using Microsoft.AspNetCore.Mvc;
using ServerSideAPI.Interfaces;

namespace ServerSideAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    public struct LoginData
    {
        public string Password { get; set; }
    }
    
    [HttpPost]
    public IActionResult Index([FromServices] ISettingsRepo  settingsRepo, [FromBody] LoginData loginData)
    {
        var receivedPass = Crypto.Hash(loginData.Password);
        if (receivedPass.Equals(settingsRepo.Current.AdminPassword))
        {
            return Accepted();
        }else if (receivedPass.Equals(settingsRepo.Current.ConnPassword))
        {
            return Ok();
        }
        else
        {
            return Unauthorized();
        }
    }
}