using JWTImplementation.Models.DTO.Request;
using JWTImplementation.Models.Model;

namespace JWTImplementation.Register.Auth
{
    public interface IAuthService
    {
        Token GenerateToken(RegisterRequest request);
    }
}