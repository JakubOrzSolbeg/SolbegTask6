using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("/")]
public class IndexController : ControllerBase
{

    [HttpGet]
    public IActionResult Index()
    {
        return Redirect("swagger/index.html");
    }
}