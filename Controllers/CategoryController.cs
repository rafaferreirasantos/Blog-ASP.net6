using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
  [ApiController]
  [Route("v1/Category")]
  public class CategoryController : ControllerBase
  {
    [HttpGet]

    public async Task<IActionResult> GetAllAsync([FromServices] BlogDataContext context)
      => Ok(await context.Categories!.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context, [FromRoute] int id)
    {
      var category = await context.Categories!.FirstOrDefaultAsync(x => x.Id == id);
      if (category == null) return NotFound();
      return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> NewCategory([FromServices] BlogDataContext context, [FromBody] Category category)
    {
      await context.Categories!.AddAsync(category);
      await context.SaveChangesAsync();
      return Created($"/Category/{category.Id}", category);
    }
  }
}
