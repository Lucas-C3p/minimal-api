using FluentValidation;
using MinimalApi.DTOs;

namespace MinimalApi.Dominio.Validadores;

public class VeiculoDTOValidator : AbstractValidator<VeiculoDTO>
{
    public VeiculoDTOValidator()
    {
        RuleFor(v => v.Nome)
            .NotEmpty().WithMessage("O nome do veículo é obrigatório")
            .Length(2, 150).WithMessage("O nome deve ter entre 2 e 150 caracteres")
            .Matches(@"^[a-zA-Z0-9\s\-]+$").WithMessage("O nome deve conter apenas letras, números, espaços e hífens");

        RuleFor(v => v.Marca)
            .NotEmpty().WithMessage("A marca do veículo é obrigatória")
            .Length(2, 100).WithMessage("A marca deve ter entre 2 e 100 caracteres")
            .Matches(@"^[a-zA-Z\s\-]+$").WithMessage("A marca deve conter apenas letras, espaços e hífens");

        RuleFor(v => v.Ano)
            .GreaterThanOrEqualTo(1950).WithMessage("O ano deve ser igual ou superior a 1950")
            .LessThanOrEqualTo(DateTime.Now.Year + 1).WithMessage($"O ano não pode ser superior a {DateTime.Now.Year + 1}");
    }
}

