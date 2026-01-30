using Api_GestionDeTurnos.Application.DTOs;
using Api_GestionDeTurnos.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_GestionDeTurnos.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TurnosController : ControllerBase
    {
        private readonly ITurnoService _service;
        private TurnosController(ITurnoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("CrearTurno", Name = "CrearTurno")]
        public async Task<IActionResult> CrearTurno([FromBody] CrearTurnoDTO createTurnoDTO)
        {
            await _service.CrearTurnoAsync(createTurnoDTO);
            return Ok();
        }
        [Authorize]
        [HttpDelete("EliminarTurno", Name = "EliminarTurno")]
        public async Task<IActionResult> EliminarTurno(Guid idTurno)
        {
            await _service.EliminarTurnoAsync(idTurno);
            return Ok();
        }
        [Authorize]
        [HttpGet("GetListaTurnosPorIdUsuario", Name = "GetListaTurnosPorIdUsuario")]
        public async Task<IActionResult> GetListaTurnosPorIdUsuario(Guid idUsuario)
        {
            var resultado = await _service.GetAllTurnosIdUsuarioAsync(idUsuario);
            if (!resultado.Any())
            {
                return BadRequest("Error al traer la lista.");
            }
            return Ok();
        }

    }
}
