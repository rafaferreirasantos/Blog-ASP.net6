using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
  public class EmailContoller : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
