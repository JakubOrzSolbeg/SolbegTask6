using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("/[action]")]
public class IndexController : ControllerBase
{

    [HttpGet]
    public IActionResult Index()
    {
        return Redirect("swagger/index.html");
    }


    [HttpGet]
    public ActionResult<int> Ping()
    {
        return new Random().Next();
    }
}