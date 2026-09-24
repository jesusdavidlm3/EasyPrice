using Microsoft.AspNetCore.Mvc;
using Models;

namespace ServerSideAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingsController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpPatch]
    public IActionResult Patch(Settings settings)
    {
        return Ok();
    }
}