using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ServerSideAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpGet]
    public IActionResult Get(string name)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post(Product product)
    {
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete(string id)
    {
        return Ok();
    }

    [HttpPatch]
    public IActionResult Patch(Product product)
    {
        return Ok();
    }
}