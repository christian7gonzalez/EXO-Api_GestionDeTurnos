namespace Api_GestionDeTurnos.Application.DTOs
{
    public class UsuarioValidadoDTO
    {
        public Guid idUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
