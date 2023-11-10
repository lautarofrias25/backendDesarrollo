using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SRVP.Data.Models;
using SRVP.Interfaces;

namespace SRVP.Helpers;

public class JWT : IJWT
{
    private readonly IConfiguration _configuration;

    public JWT(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Persona user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.usuario),
            new Claim(ClaimTypes.Email, user.email),
            new Claim(ClaimTypes.Role, user.rol)
        };
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:ClavePrivada").Get<string>() ?? string.Empty));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
