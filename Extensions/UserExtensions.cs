using Blog.Models;
using System.Linq;
using System.Security.Claims;

namespace Blog.Extensions;
public static class UserExtensions
{
  public static IEnumerable<Claim> GetClaims(this User user)
  {
    List<Claim> claims = new();
    claims.Add(new Claim(ClaimTypes.Name, user.Email));
    claims.AddRange(user.Roles.Select(x => new Claim(ClaimTypes.Role, x.Name)).ToArray());
    return claims;
  }
}
