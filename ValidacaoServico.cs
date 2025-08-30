using FluentValidation;
using MinimalApi.Dominio.ModelViews;

namespace MinimalApi.Dominio.Servicos;

public class ValidacaoServico
{
    private readonly IServiceProvider _serviceProvider;

    public ValidacaoServico(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<ErrosDeValidacao?> ValidarAsync<T>(T objeto) where T : class
    {
        var validator = _serviceProvider.GetService<IValidator<T>>();
        if (validator == null)
            return null;

        var resultado = await validator.ValidateAsync(objeto);
        
        if (!resultado.IsValid)
        {
            return new ErrosDeValidacao
            {
                Mensagens = resultado.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        return null;
    }

    public ErrosDeValidacao? Validar<T>(T objeto) where T : class
    {
        var validator = _serviceProvider.GetService<IValidator<T>>();
        if (validator == null)
            return null;

        var resultado = validator.Validate(objeto);
        
        if (!resultado.IsValid)
        {
            return new ErrosDeValidacao
            {
                Mensagens = resultado.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        return null;
    }
}

