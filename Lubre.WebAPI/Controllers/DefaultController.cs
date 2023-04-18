using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lubre.WebAPI.Controllers;

[ApiController]
[Route("/")]
public class DefaultController : ControllerBase
{
    HttpClient client = new HttpClient();
    /// <summary>
    /// Default controller
    /// </summary>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Lubre.WebAPI/Swagger");
    }
}
