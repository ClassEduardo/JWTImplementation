using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTImplementation.Models.DTO.Request;
using JWTImplementation.Models.Model;
using Microsoft.IdentityModel.Tokens;

namespace JWTImplementation.Register.Auth;

public class AuthService(
    IConfiguration configuration
) : IAuthService
{
    private readonly IConfiguration _configuration = configuration;
    public Token GenerateToken(RegisterRequest request)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!);
        var expiresAt = DateTime.Now.AddHours(24);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, request.Name),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Role, request.Role),
            }),
            Expires = expiresAt,
            Issuer = _configuration["Jwt:SecretKey"]!,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new Token
        {
            AccessToken = tokenHandler.WriteToken(token)
        };
    }
}