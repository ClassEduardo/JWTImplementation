using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTImplementation.Models.DTO.Response
{
    public class ValidateTokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}