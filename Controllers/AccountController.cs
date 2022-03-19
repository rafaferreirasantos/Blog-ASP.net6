using Blog.Data;
using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Blog.Extensions;
using SecureIdentity.Password;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{

  [ApiController]
  [Route("/v1/account")]
  public class AccountController : ControllerBase
  {
    private readonly TokenService tokenService;
    public AccountController(TokenService tokenService)
    {
      this.tokenService = tokenService;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(
      [FromServices] BlogDataContext context, 
      [FromBody] LoginViewModel viewModel)
    {
      var user = await context
        .Users
        .AsNoTracking()
        .Include(u => u.Roles)
        .FirstOrDefaultAsync(u => u.Email == viewModel.Email);

      if (user == null || !PasswordHasher.Verify(user.PasswordHash, viewModel.Password)) return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválida."));
      var token = tokenService.GenerateToken(user);
      return Ok(token);
    }
    [HttpPost()]
    public async Task<IActionResult> NewUser(
      [FromServices] BlogDataContext context,
      [FromBody] RegisterViewModel viewModel
      )
    {
      if (!ModelState.IsValid) return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));
      try
      {
        var user = new User
        {
          Bio = "",
          Email = viewModel.Email!,
          Image = "",
          Name = viewModel.Name!,
          PasswordHash = PasswordHasher.Hash(viewModel.Password),
          Slug = viewModel.Email!.Replace("@", "-").Replace(".", "-")
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return Created($"account/{user.Id}", new ResultViewModel<dynamic>(new
        {
          name = user.Name,
          email = user.Email
        }));
      }
      catch (DbUpdateException) { return BadRequest(new ResultViewModel<User>("Erro cadastrando novo usuário.")); }
      catch (Exception) { return BadRequest(new ResultViewModel<User>("Erro cadastrando novo usuário.")); }
    }
  }
}
