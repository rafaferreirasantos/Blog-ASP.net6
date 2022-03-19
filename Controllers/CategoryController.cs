using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
  [Authorize]
  [ApiController]
  [Route("v1/category")]
  public class CategoryController : ControllerBase
  {
    [HttpGet]

    public async Task<IActionResult> GetAllAsync([FromServices] BlogDataContext context)
      => Ok(await context.Categories.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context, [FromRoute] int id)
    {
      var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
      if (category == null) return NotFound();
      return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> NewCategory(
      [FromServices] BlogDataContext context,
      [FromBody] EditorCategoryViewModel category)
    {
      var newCategory = new Category()
      {
        Id = 0,
        Name = category.Name,
        Slug = category.Slug,
      };
      try
      {
        await context.Categories.AddAsync(newCategory);
        await context.SaveChangesAsync();
      }
      catch (DbUpdateException) { return BadRequest("Impossible to add the new category"); }
      catch (Exception) { return StatusCode(500, "Internal server error"); }

      return Created($"/Category/{newCategory.Id}", newCategory);
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCategory(
        [FromServices] BlogDataContext context,
        [FromBody] EditorCategoryViewModel model,
        [FromRoute] int id)
    {
      var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
      if (category == null) return NotFound();

      category.Slug = model.Slug;
      category.Name = model.Name;

      try
      {
        context.Update(category);
        context.SaveChanges();
      }
      catch (DbUpdateException) { return BadRequest("Impossible to update category"); }
      catch (Exception) { return StatusCode(500, "Internal server error"); };
      return Ok(category);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id, [FromServices] BlogDataContext context)
    {
      var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
      if (category == null) return NotFound();

      try
      {
        context.Categories.Remove(category);
        context.SaveChanges();
      }
      catch (DbUpdateException) { return BadRequest("Impossible to delete category"); }
      catch (Exception) { return StatusCode(500, "Internal server error"); }
      return Ok(category);
    }
  }
}
