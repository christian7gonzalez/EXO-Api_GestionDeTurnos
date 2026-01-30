using System.ComponentModel.DataAnnotations;

namespace Api_GestionDeTurnos.Application.DTOs
{
    public class LoginDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
