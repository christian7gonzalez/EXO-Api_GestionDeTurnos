namespace Api_GestionDeTurnos.Application.DTOs
{
    public class CrearTurnoDTO
    {
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaTurno { get; set; }
        public Guid IdUsuario { get; set; }
    }
}
