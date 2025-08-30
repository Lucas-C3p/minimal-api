using FluentValidation;
using MinimalApi.DTOs;
using MinimalApi.Dominio.Enuns;

namespace MinimalApi.Dominio.Validadores;

public class AdministradorDTOValidator : AbstractValidator<AdministradorDTO>
{
    public AdministradorDTOValidator()
    {
        RuleFor(a => a.Email)
            .NotEmpty().WithMessage("O email é obrigatório")
            .EmailAddress().WithMessage("O email deve ter um formato válido")
            .MaximumLength(255).WithMessage("O email deve ter no máximo 255 caracteres");

        RuleFor(a => a.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória")
            .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres")
            .MaximumLength(100).WithMessage("A senha deve ter no máximo 100 caracteres")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$")
            .WithMessage("A senha deve conter pelo menos uma letra minúscula, uma maiúscula e um número");

        RuleFor(a => a.Perfil)
            .NotNull().WithMessage("O perfil é obrigatório")
            .IsInEnum().WithMessage("O perfil deve ser um valor válido (Adm ou Editor)");
    }
}

