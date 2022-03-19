using Blog.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Services;
public class TokenService
{
  public string GenerateToken(User user)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(Configuration.JWTKey);
    var tokenDescriptior = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new Claim[]
      {
        new (ClaimTypes.Name, user.Name)
      }),
      Expires = DateTime.UtcNow.AddMinutes(120),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    foreach(Role r in user.Roles)
    {
      tokenDescriptior.Subject.AddClaim(new Claim(ClaimTypes.Role, r.Name));
    }

    var token = tokenHandler.CreateToken(tokenDescriptior);
    return tokenHandler.WriteToken(token);
  }
}
