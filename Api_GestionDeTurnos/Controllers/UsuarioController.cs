using Api_GestionDeTurnos.Application.DTOs;
using Api_GestionDeTurnos.Application.Interfaces;
using Api_GestionDeTurnos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_GestionDeTurnos.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _service;
        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("CrearUsuario", Name = "CrearUsuario")]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            await _service.CrearUsuarioAsync(usuarioDTO);
            return Ok();
        }
        [AllowAnonymous]
        [HttpPost("Login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody]LoginDTO loginDTO)
        {
            var resultado =  await _service.LoginAsync(loginDTO);
            if (resultado is null)
                return NotFound(new { message = "Login incorrecto" });

            return Ok(resultado);

        }
        [Authorize]
        [HttpDelete("EliminarUsuario", Name = "EliminarUsuario")]
        public async Task<IActionResult> EliminarUsuario(Guid idUsuario)
        {
            await _service.EliminarUsuario(idUsuario);
            return Ok();

        }
    }
}
