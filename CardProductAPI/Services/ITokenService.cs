using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CardProductAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace CardProductAPI.Services;

public interface ITokenService
{
    string GenerateToken(string key, string issuer, User userModel);
}

public class TokenService : ITokenService
{
    private const double EXP_DURATION_MINUTES = 30;

    public string GenerateToken(string key, string issuer, User userModel)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userModel.Email),
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(issuer: issuer, audience: issuer, claims, expires: DateTime.Now.AddMinutes(EXP_DURATION_MINUTES), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}