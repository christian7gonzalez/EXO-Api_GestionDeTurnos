using Api_GestionDeTurnos.Domain.Entities;

namespace Api_GestionDeTurnos.Domain.Interfaces
{
    public interface ITurnoRepository
    {
        Task<Turno> AddAsync(Turno turno);
        Task<Turno?> GetByIdAsync(Guid id);
        Task<List<Turno>> GetAllTurnosByIdUsuarioAsync(Guid idUsuario);
        Task DeleteAsync(Guid id);
    }
}
