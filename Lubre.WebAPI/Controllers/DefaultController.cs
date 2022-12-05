using Microsoft.AspNetCore.Mvc;

namespace Lubre.WebAPI.Controllers;

[ApiController]
[Route("/")]
public class DefaultController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Lubre.WebAPI");
    }
}
