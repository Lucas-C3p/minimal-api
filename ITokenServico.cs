using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Dominio.Interfaces;

public interface ITokenServico
{
    /// <summary>
    /// Gera um token JWT para o administrador
    /// </summary>
    /// <param name="administrador">Administrador para o qual gerar o token</param>
    /// <returns>Token JWT como string</returns>
    string GerarToken(Administrador administrador);

    /// <summary>
    /// Valida se um token JWT é válido
    /// </summary>
    /// <param name="token">Token a ser validado</param>
    /// <returns>True se o token for válido, false caso contrário</returns>
    bool ValidarToken(string token);
}

