using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchConsulting.GestorInventario.Application.Dto;

namespace TouchConsulting.GestorInventario.Application.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres.");

            RuleFor(user => user.LastName)
                .MinimumLength(2).When(user => !string.IsNullOrEmpty(user.LastName))
                .WithMessage("El apellido debe tener al menos 2 caracteres.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("El correo es requerido.")
                .EmailAddress().WithMessage("Debe proporcionar un correo válido.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("La contraseña es requerida.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .Matches(@"[A-Za-z]").WithMessage("La contraseña debe incluir al menos una letra.")
                .Matches(@"\d").WithMessage("La contraseña debe incluir al menos un número.");

        }

    }
}
