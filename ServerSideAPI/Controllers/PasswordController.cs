using Libs;
using Microsoft.AspNetCore.Mvc;
using ServerSideAPI.Interfaces;

namespace ServerSideAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PasswordController : Controller
{
    public struct UpdatePasswordDTO
    {
        public string NewPassword { get; set; }
    }
    
    [HttpPatch]
    [Route("conn")]
    public IActionResult ChangeConnPassword([FromServices] ISettingsRepo settingsRepo, [FromBody] UpdatePasswordDTO updatePasswordDto)
    {
        var response = settingsRepo.UpdateConnPassword(updatePasswordDto.NewPassword);
        if (response)
        {
            return Accepted();
        }
        
        return BadRequest("Las contraseñas de Administracion y coneccion no pueden ser iguales");
    }
    
    [HttpPatch]
    [Route("admin")]
    public IActionResult ChangeAdminPassword([FromServices] ISettingsRepo settingsRepo, [FromBody] UpdatePasswordDTO updatePasswordDto)
    {
        var response = settingsRepo.UpdateAdminPassword(updatePasswordDto.NewPassword);
        if (response)
        {
            return Accepted();
        }

        return BadRequest("Las contraseñas de Administracion y coneccion no pueden ser iguales");
    }
}