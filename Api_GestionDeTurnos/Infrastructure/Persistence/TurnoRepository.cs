using Api_GestionDeTurnos.Domain.Entities;
using Api_GestionDeTurnos.Domain.Interfaces;
using Api_GestionDeTurnos.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Api_GestionDeTurnos.Infrastructure.Persistence
{
    public class TurnoRepository : ITurnoRepository
    {
        private readonly GestionDeTurnosDbContext _dbContext;
        public TurnoRepository(GestionDeTurnosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Turno> AddAsync(Turno turno)
        {
            await _dbContext.Turnos.AddAsync(turno);
            await _dbContext.SaveChangesAsync();
            return turno;
        }

        public async Task DeleteAsync(Guid id)
        {
            var turno = await GetByIdAsync(id);
            if (turno is null)
            {
                throw new InfrastructureException($"Turno con Id {id} no existe.");
            }
            _dbContext.Turnos.Remove(turno);
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<Turno>> GetAllTurnosByIdUsuarioAsync(Guid idUsuario)
        {
            return _dbContext.Turnos.AsNoTracking().Where(u => u.IdUsuario == idUsuario).ToListAsync();
        }

        public Task<Turno?> GetByIdAsync(Guid id)
        {
            return _dbContext.Turnos.AsNoTracking().FirstOrDefaultAsync(t => t.IdTurno == id);
        }
    }
}
