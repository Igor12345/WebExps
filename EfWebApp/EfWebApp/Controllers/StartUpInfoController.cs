using EfWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EfWebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class StartUpInfoController : Controller
{
    private readonly StartupInfo _startupInfo;

    public StartUpInfoController(Func<StartupInfo> startupInfoProvider)
    {
        _startupInfo = startupInfoProvider();
    }
    
    [HttpGet(Name = "GetStartUpInfo")]
    public IActionResult Get()
    {
        return _startupInfo.Success ? Ok("success") : Ok(new { error = true, message = _startupInfo.ErrorMessage });
    }
}