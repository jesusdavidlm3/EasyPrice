using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Models;
using ServerSideAPI.Interfaces;
using ServerSideAPI.Middlewares;

namespace ServerSideAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController : Controller
{
    
    
    [HttpGet]
    [TypeFilter(typeof(AuthConn))]
    public IActionResult Get([FromServices] IProductsRepo productsRepo)
    {
        var response = JsonSerializer.Serialize(productsRepo.GetProducts());
        return Ok(response);
    }

    [HttpGet]
    [Route("{date}")]
    [TypeFilter(typeof(AuthConn))]
    public IActionResult Update([FromServices] IProductsRepo productsRepo, [FromRoute] DateTime date)
    {
        var response = JsonSerializer.Serialize(productsRepo.GetProducts(date));
        return Ok(response);
    }

    [HttpPost]
    [TypeFilter(typeof(AuthAdmin))]
    public IActionResult Post([FromServices] IProductsRepo productsRepo, [FromBody]Product product)
    {
        var response = productsRepo.SaveProduct(product);
        return Created("", response);
    }

    [HttpDelete]
    [Route("{id}")]
    [TypeFilter(typeof(AuthAdmin))]
    public IActionResult Delete([FromServices] IProductsRepo productsRepo, [FromRoute]Guid id)
    {
        productsRepo.DeleteProduct(id);
        return Ok();
    }

    [HttpPatch]
    [TypeFilter(typeof(AuthAdmin))]
    public IActionResult Patch([FromServices] IProductsRepo productsRepo, [FromBody]Product product)
    {
        var response = productsRepo.EditProduct(product);
        return Accepted(response);
    }
}