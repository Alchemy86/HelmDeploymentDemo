using Microsoft.AspNetCore.Mvc;

namespace HelmDeploymentDemo.Controllers;

[ApiController]
[Route("")]
public class DefaultController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Alive and well");
    }
}