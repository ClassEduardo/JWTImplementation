using JWTImplementation.Models.Model;

namespace JWTImplementation.Models.DTO.Response
{
    public class TokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = "Bearer";
        public DateTime ExpiresAt { get; set; }
    }
}
