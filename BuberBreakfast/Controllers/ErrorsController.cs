using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

[Route("[controller]")]
public class ErrorsController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Problem();
    }
}