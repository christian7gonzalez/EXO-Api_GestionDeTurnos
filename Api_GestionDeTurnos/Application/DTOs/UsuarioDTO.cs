namespace Api_GestionDeTurnos.Application.DTOs
{
    public class UsuarioDTO
    {
        public Guid? IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public string FechaNacimiento { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
