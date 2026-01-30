using Api_GestionDeTurnos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Api_GestionDeTurnos.Infrastructure.Persistence.EntityConfigurations.Schema;


namespace Api_GestionDeTurnos.Infrastructure.Persistence.EntityConfigurations
{
    public class UsuarioEntityConfigurations : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios", USUARIOS);

            builder.HasKey(x => x.IdUsuario);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Nombre).HasMaxLength(100);
            builder.Property(x => x.Apellido).HasMaxLength(100);
            builder.Property(x => x.Sexo).HasMaxLength(15);    
            builder.Property(x => x.FechaAlta).IsRequired();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(300);
 
        }
    }
}
