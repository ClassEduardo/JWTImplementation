using System.Security.Claims;
using JWTImplementation.Models.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTImplementation.Validator.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Requer autenticação
public class ValidatorController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        // Pega dados do usuário autenticado
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        return Ok(new
        {
            message = "Lista de produtos",
            userName,
            userEmail,
            userRole
        });
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")] // Apenas admin
    public IActionResult Create([FromBody] object produto)
    {
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

        return Ok(new { message = $"Validado + {userEmail}" });
    }
}