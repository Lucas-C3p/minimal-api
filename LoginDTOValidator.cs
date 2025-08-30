using FluentValidation;
using MinimalApi.DTOs;

namespace MinimalApi.Dominio.Validadores;

public class LoginDTOValidator : AbstractValidator<LoginDTO>
{
    public LoginDTOValidator()
    {
        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("O email é obrigatório")
            .EmailAddress().WithMessage("O email deve ter um formato válido");

        RuleFor(l => l.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória")
            .MinimumLength(1).WithMessage("A senha não pode estar vazia");
    }
}

