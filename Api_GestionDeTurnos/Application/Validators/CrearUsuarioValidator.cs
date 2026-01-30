using Api_GestionDeTurnos.Application.DTOs;
using FluentValidation;

namespace Api_GestionDeTurnos.Application.Validators
{
    public class CrearUsuarioValidator : AbstractValidator<UsuarioDTO>
    {
        public CrearUsuarioValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("Formato de email inválido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");
        }

    }
}
