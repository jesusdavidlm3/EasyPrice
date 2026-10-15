using Microsoft.AspNetCore.Mvc;

namespace ServerSideAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PowerController : Controller
{
    [HttpDelete]
    public IActionResult Delete()
    {
        return Ok();
    }
}