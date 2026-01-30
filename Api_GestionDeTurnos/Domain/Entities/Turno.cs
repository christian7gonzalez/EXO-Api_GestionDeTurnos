using Api_GestionDeTurnos.Domain.Enum;
using Api_GestionDeTurnos.Domain.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_GestionDeTurnos.Domain.Entities
{
    public class Turno
    {
        public Guid IdTurno { get; set; } = Guid.NewGuid();
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaAlta { get; set; } = DateTime.Now;
        public DateTime FechaTurno { get; set; }

        [ForeignKey("Usuario")]
        public Guid IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public string Estado { get; set; } = Enum.EstadoEnum.Pendiente.ToString();

        public void Reprogramar(DateTime nuevaFecha)
        {
            if (nuevaFecha < DateTime.Now)
                throw new DomainException("Fecha inválida");

            FechaTurno = nuevaFecha;
        }
        public static Turno Crear(Guid idUsuario, DateTime fechaTurno, string descripcion)
        {
            if (idUsuario == Guid.Empty)
                throw new DomainException("El usuario es requerido");

            return new Turno
            {
                IdTurno = Guid.NewGuid(),
                IdUsuario = idUsuario,
                FechaTurno = fechaTurno,
                Descripcion = descripcion,
                Estado = EstadoEnum.Pendiente.ToString()
            };
        }
    }
}
