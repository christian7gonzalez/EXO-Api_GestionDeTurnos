using Api_GestionDeTurnos.Application.DTOs;
using FluentValidation;

namespace Api_GestionDeTurnos.Application.Validators
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("Formato de email inválido");

        }
    }
}
