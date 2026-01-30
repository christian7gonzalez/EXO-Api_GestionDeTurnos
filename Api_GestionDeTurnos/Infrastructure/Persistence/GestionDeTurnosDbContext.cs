using Api_GestionDeTurnos.Domain.Entities;
using Api_GestionDeTurnos.Infrastructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Api_GestionDeTurnos.Infrastructure.Persistence
{
    public class GestionDeTurnosDbContext : DbContext
    {
        public GestionDeTurnosDbContext(DbContextOptions<GestionDeTurnosDbContext> options) : base(options) {}

        public DbSet<Turno> Turnos => Set<Turno>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TurnoEntityConfigurations());
            modelBuilder.ApplyConfiguration(new UsuarioEntityConfigurations());
        }
    }
}
