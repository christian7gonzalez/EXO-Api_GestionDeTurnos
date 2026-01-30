using Api_GestionDeTurnos.Application.DTOs;
using Api_GestionDeTurnos.Domain.Entities;

namespace Api_GestionDeTurnos.Application.Interfaces
{
    public interface ITurnoService
    {
        Task<Turno> CrearTurnoAsync(CrearTurnoDTO createTurnoDTO);
        Task EliminarTurnoAsync(Guid idTurno);
        Task<List<Turno>> GetAllTurnosIdUsuarioAsync(Guid idUsuario);
        
    }
}
