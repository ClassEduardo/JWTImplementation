using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //Verifique quem emitiu esse token.
        ValidateLifetime = true, //Verifica expiração do token.
        ValidateIssuerSigningKey = true, //Confirme que o token foi assinado com a chave correta.
        ValidateAudience = false, // 
        ValidIssuer = jwtSettings["Issuer"], // Quem emitiu o token
        ValidAudience = jwtSettings["Audience"], // É para mim?
        IssuerSigningKey = new SymmetricSecurityKey(secretKey) //Define qual chave é usada para validar a assinatura.
    };
});

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapOpenApi();
app.MapScalarApiReference();
app.MapControllers();

app.Run();

