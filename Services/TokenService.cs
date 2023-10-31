using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtBearerAspNet.Models;
using Microsoft.IdentityModel.Tokens;

namespace JwtBearerAspNet.Services;

public class TokenService
{
  public string Generete(User user)
  {
    var handler = new JwtSecurityTokenHandler();

    var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);
    var credentials = new SigningCredentials(
      new SymmetricSecurityKey(key),
      SecurityAlgorithms.HmacSha256Signature
    );

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = GenerateClaims(user),
      SigningCredentials = credentials,
      Expires = DateTime.UtcNow.AddHours(2)
    };

    var token = handler.CreateToken(tokenDescriptor);

    var strToken = handler.WriteToken(token);

    return strToken;
  }

  static ClaimsIdentity GenerateClaims(User user)
  {
    var ci = new ClaimsIdentity();

    ci.AddClaim(
      new Claim(ClaimTypes.Name, user.Email)
    );

    foreach (var role in user.Roles)
    {
      ci.AddClaim(
        new Claim(ClaimTypes.Role, role)
      );
    }

    return ci;
  }
}
