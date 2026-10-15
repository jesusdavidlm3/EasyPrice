using Microsoft.AspNetCore.Mvc;
using ServerSideAPI.Interfaces;

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
    public IActionResult GetDollar([FromServices] ISettingsRepo settingsRepo)
    {
        return Ok(settingsRepo.Current.DolarPrice);
    }
    
    [HttpPatch]
    public IActionResult UpdateDollar([FromServices] ISettingsRepo settingsRepo, [FromBody] UpdateDolarDTO dollar)
    {
        settingsRepo.UpdateDolarPrice(dollar.DolarPrice);
        return Ok();
    }
}