using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;

namespace MinimalApi.Dominio.Servicos;

public class TokenServico : ITokenServico
{
    private readonly IConfiguration _configuration;

    public TokenServico(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GerarToken(Administrador administrador)
    {
        var key = _configuration.GetSection("Jwt:Key").Value ?? 
                  _configuration.GetSection("Jwt").Value ?? 
                  "minimal-api-default-key-32-chars-min";

        if (string.IsNullOrEmpty(key))
            return string.Empty;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, administrador.Id.ToString()),
            new Claim(ClaimTypes.Email, administrador.Email),
            new Claim("Perfil", administrador.Perfil),
            new Claim(ClaimTypes.Role, administrador.Perfil),
        };

        var expirationHours = _configuration.GetValue<int>("Jwt:ExpirationInHours", 24);
        
        var token = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("Jwt:Issuer"),
            audience: _configuration.GetValue<string>("Jwt:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddHours(expirationHours),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidarToken(string token)
    {
        try
        {
            var key = _configuration.GetSection("Jwt:Key").Value ?? 
                      _configuration.GetSection("Jwt").Value ?? 
                      "minimal-api-default-key-32-chars-min";

            if (string.IsNullOrEmpty(key))
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
}

