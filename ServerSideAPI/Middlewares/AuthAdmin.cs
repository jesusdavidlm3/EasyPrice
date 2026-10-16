using Libs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ServerSideAPI.Interfaces;

namespace ServerSideAPI.Middlewares;

public class AuthAdmin :  IActionFilter
{
    private ISettingsRepo _settingsRepo;
    
    public AuthAdmin(ISettingsRepo settingsRepo)
    {
        _settingsRepo = settingsRepo;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        context.HttpContext.Request.Headers.TryGetValue("Authorization", out var pass);
        if (!pass.Equals(_settingsRepo.Current.AdminPassword))
        {
            context.Result = new UnauthorizedResult();
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}