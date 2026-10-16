using Microsoft.AspNetCore.Mvc;
using ServerSideAPI.Interfaces;
using ServerSideAPI.Middlewares;

namespace ServerSideAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DollarController : Controller
{
    public struct UpdateDolarDTO
    {
        public Double DolarPrice { get; set; }
    }
    
    [HttpGet]
    [TypeFilter(typeof(AuthConn))]
    public IActionResult GetDollar([FromServices] ISettingsRepo settingsRepo)
    {
        return Ok(settingsRepo.Current.DolarPrice);
    }
    
    [HttpPatch]
    [TypeFilter(typeof(AuthAdmin))]
    public IActionResult UpdateDollar([FromServices] ISettingsRepo settingsRepo, [FromBody] UpdateDolarDTO dollar)
    {
        settingsRepo.UpdateDolarPrice(dollar.DolarPrice);
        return Ok();
    }
}