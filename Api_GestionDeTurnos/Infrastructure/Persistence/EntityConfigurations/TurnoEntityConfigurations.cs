using Api_GestionDeTurnos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Api_GestionDeTurnos.Infrastructure.Persistence.EntityConfigurations.Schema;


namespace Api_GestionDeTurnos.Infrastructure.Persistence.EntityConfigurations
{
    public class TurnoEntityConfigurations : IEntityTypeConfiguration<Turno>
    {
        public void Configure(EntityTypeBuilder<Turno> builder)
        {
            builder.ToTable("Turnos", TURNOS);

            builder.HasKey(x => x.IdTurno);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Descripcion).HasMaxLength(1000);
            builder.Property(x => x.FechaAlta).IsRequired();
            builder.Property(x => x.FechaTurno).IsRequired();

            builder.Property(x => x.IdUsuario).IsRequired();

            builder.Property(x => x.Estado).HasConversion<string>().HasMaxLength(50);
        }
    }
}
