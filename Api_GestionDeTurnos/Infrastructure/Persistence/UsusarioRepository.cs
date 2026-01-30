using Api_GestionDeTurnos.Domain.Entities;
using Api_GestionDeTurnos.Domain.Interfaces;
using Api_GestionDeTurnos.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Api_GestionDeTurnos.Infrastructure.Persistence
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly GestionDeTurnosDbContext _dbContext;
        public UsuarioRepository(GestionDeTurnosDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Usuario?> AddAsync(Usuario usuario)
        {
            await _dbContext.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }

        public async Task DeleteAsync(Guid id)
        {
            var usuario = await GetByIdAsync(id);
            if (usuario is null)
            {
                throw new InfrastructureException($"Usuario con Id {id} no existe.");
            }
            _dbContext.Usuarios.Remove(usuario);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _dbContext.Usuarios.AsNoTracking().ToListAsync();
        }


        public Task<Usuario?> GetByIdAsync(Guid id)
        {
            return _dbContext.Usuarios.AsNoTracking().FirstOrDefaultAsync(t => t.IdUsuario == id);
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _dbContext.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<Usuario?> UpdateAsync(Usuario usuario)
        {
            _dbContext.Usuarios.Update(usuario);
            return Task.FromResult<Usuario?>(usuario);
        }
    }
}
