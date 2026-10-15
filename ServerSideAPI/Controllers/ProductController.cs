using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Models;
using ServerSideAPI.Interfaces;

namespace ServerSideAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController : Controller
{
    
    [HttpGet]
    public IActionResult Get([FromServices] IProductsRepo productsRepo)
    {
        var response = JsonSerializer.Serialize(productsRepo.GetProducts());
        return Ok(response);
    }

    [HttpPost]
    public IActionResult Post([FromServices] IProductsRepo productsRepo, [FromBody]Product product)
    {
        var response = productsRepo.SaveProduct(product);
        return Created("", response);
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete([FromServices] IProductsRepo productsRepo, [FromRoute]Guid id)
    {
        productsRepo.DeleteProduct(id);
        return Ok();
    }

    [HttpPatch]
    public IActionResult Patch([FromServices] IProductsRepo productsRepo, [FromBody]Product product)
    {
        var response = productsRepo.EditProduct(product);
        return Accepted(response);
    }
}