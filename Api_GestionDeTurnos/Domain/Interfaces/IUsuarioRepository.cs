using Api_GestionDeTurnos.Domain.Entities;

namespace Api_GestionDeTurnos.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> AddAsync(Usuario turno);
        Task<Usuario?> GetByIdAsync(Guid id);
        Task<List<Usuario>> GetAllAsync();
        Task DeleteAsync(Guid id);
        Task<Usuario?> GetByEmailAsync(string email);
        Task<Usuario?> UpdateAsync(Usuario usuario);
    }
}
