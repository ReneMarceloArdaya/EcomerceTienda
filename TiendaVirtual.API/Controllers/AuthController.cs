using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TiendaVirtual.API.Data;
using TiendaVirtual.API.Models;
using TiendaVirtual.API.Models.DTOs;

namespace TiendaVirtual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TiendavirtualContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(TiendavirtualContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //login con Token JWT

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == dto.Correo
                );

            if (usuario == null)
                return Unauthorized("Usuario no encontrado");

            bool esValida = BCrypt.Net.BCrypt.Verify(dto.Contrasena, usuario.Contrasena);

            if (!esValida)
                return Unauthorized("Contraseña incorrecta");

            var token = GenerateJwtToken(usuario);

            return Ok(new
            {
                Token = token,
                Usuario = new
                {
                    usuario.Id,
                    usuario.NombreUsuario,
                    usuario.Correo,
                    usuario.TipoUsuario
                }
            }
            );
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var claims = new[]
            {
            new Claim("sub", usuario.Id.ToString()),           // User ID
            new Claim("email", usuario.Correo ?? ""),          // Email
            new Claim("name", usuario.NombreUsuario ?? ""),    // Name
            new Claim("role", usuario.TipoUsuario ?? "Cliente") // Role (formato simple)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"] ?? "Clave_Secreta_Muy_Larga_y_Segura_12345"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(double.Parse(_configuration["Jwt:ExpireHours"] ?? "24")),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
