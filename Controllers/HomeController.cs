using Blog.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Route("v1")]
    public class HomeController : ControllerBase
    {
        [HttpGet("status")]
        [Route("/")]
        public IActionResult HealthCheck([FromServices] IConfiguration config)
          => Ok(new
          {
              msg = "Status 200: API running...",
              environment = config.GetValue<string>("Env")
          });
        [ApiKey]
        [HttpGet("apikeytest")]
        public IActionResult APIKeyTest()
        => Ok("Success.");
    }
}
