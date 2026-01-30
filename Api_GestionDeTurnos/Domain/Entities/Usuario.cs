using Api_GestionDeTurnos.Domain.Exceptions;
using Api_GestionDeTurnos.Domain.Helpers;

namespace Api_GestionDeTurnos.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public Guid IdUsuario { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; //Es el usuario con el cual ingresa a la aplicación

        public string Sexo { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public DateTime FechaAlta { get; set; } = DateTime.Now;
        public DateTime? FechaBaja { get; set; }
        public string Password { get; set; } = string.Empty;

        public void ActualizarEmail(string email)
        {
            if (!DomainHelpers.EsEmailValido(email))
                throw new DomainException("Email inválido");

            Email = email;
        }
        public void ActualizarNombre(string nombre)
        {
            if (!string.Equals(nombre, Nombre, StringComparison.CurrentCultureIgnoreCase))
            {
                Nombre = nombre;
            }   
        }
        public void ActualizarApellido(string apellido)
        {
            if (!string.Equals(apellido, Apellido, StringComparison.CurrentCultureIgnoreCase))
            {
                Apellido = apellido;
            }
        }


    }
}
