using Microsoft.AspNetCore.Mvc;

namespace ServerSideAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    [HttpPost]
    public IActionResult Index()
    {
        return Ok();
    }
}