using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers 
{
  [ApiController]
  [Route("v1")]
  public class HomeController : ControllerBase
  {
    [HttpGet("status")]
    [Route("/")]
    public IActionResult HealthCheck() 
      => Ok("Status 200: API running...");

  }
}
