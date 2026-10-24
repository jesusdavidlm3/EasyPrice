using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Models;
using ServerSideAPI.Interfaces;
using ServerSideAPI.Middlewares;
using ServerSideAPI.Repositories;

namespace ServerSideAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController : Controller
{
    [HttpGet]
    [TypeFilter(typeof(AuthConn))]
    public IActionResult Get([FromServices] IProductsRepo productsRepo)
    {
        var response = productsRepo.GetProducts();
        return Ok(response);
    }

    [HttpGet]
    [Route("{ticks}")]
    [TypeFilter(typeof(AuthConn))]
    public IActionResult Update([FromServices] IProductsRepo productsRepo, [FromServices] ISettingsRepo settingsRepo, [FromRoute] long ticks)
    {
        var date = new DateTime(ticks);
        List<Product> response;
        if (settingsRepo.Current.LastUpdate > date && !settingsRepo.Current.LastUpdate.Equals(date))
        {
            response = productsRepo.GetProducts(date);
        }
        else
        {
            response = new List<Product>();
        }
        return Ok(response);
    }

    [HttpPost]
    [TypeFilter(typeof(AuthAdmin))]
    public IActionResult Post([FromServices] IProductsRepo productsRepo, [FromServices] ISettingsRepo settingsRepo, [FromBody]Product product)
    {
        var response = productsRepo.SaveProduct(product);
        settingsRepo.UpdateLastUpdate(product.LastUpdate ?? DateTime.Now);
        return Created("", response);
    }

    [HttpDelete]
    [Route("{id}")]
    [TypeFilter(typeof(AuthAdmin))]
    public IActionResult Delete([FromServices] IProductsRepo productsRepo, [FromServices] ISettingsRepo settingsRepo, [FromRoute]Guid id)
    {
        var newLastUpdate = productsRepo.DeleteProduct(id);
        settingsRepo.UpdateLastUpdate(newLastUpdate);
        return Ok(newLastUpdate);
    }

    [HttpPatch]
    [TypeFilter(typeof(AuthAdmin))]
    public IActionResult Patch([FromServices] IProductsRepo productsRepo, [FromServices] ISettingsRepo settingsRepo, [FromBody]Product product)
    {
        var response = productsRepo.EditProduct(product);
        settingsRepo.UpdateLastUpdate(response.LastUpdate ?? DateTime.Now);
        return Accepted(response);
    }
}