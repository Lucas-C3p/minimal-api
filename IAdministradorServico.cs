using MinimalApi.Dominio.Entidades;
using MinimalApi.DTOs;

namespace MinimalApi.Dominio.Interfaces;

public interface IAdministradorServico
{
    /// <summary>
    /// Realiza o login de um administrador
    /// </summary>
    /// <param name="loginDTO">Dados de login (email e senha)</param>
    /// <returns>Administrador logado ou null se as credenciais forem inválidas</returns>
    Administrador? Login(LoginDTO loginDTO);

    /// <summary>
    /// Inclui um novo administrador no sistema
    /// </summary>
    /// <param name="administrador">Dados do administrador a ser incluído</param>
    /// <returns>Administrador incluído com ID gerado</returns>
    Administrador Incluir(Administrador administrador);

    /// <summary>
    /// Busca um administrador pelo ID
    /// </summary>
    /// <param name="id">ID do administrador</param>
    /// <returns>Administrador encontrado ou null</returns>
    Administrador? BuscaPorId(int id);

    /// <summary>
    /// Lista todos os administradores com paginação
    /// </summary>
    /// <param name="pagina">Número da página (padrão: 1)</param>
    /// <param name="tamanhoPagina">Tamanho da página (padrão: 10)</param>
    /// <returns>Lista de administradores</returns>
    List<Administrador> Todos(int? pagina = 1, int tamanhoPagina = 10);

    /// <summary>
    /// Atualiza os dados de um administrador
    /// </summary>
    /// <param name="administrador">Administrador com dados atualizados</param>
    void Atualizar(Administrador administrador);

    /// <summary>
    /// Remove um administrador do sistema
    /// </summary>
    /// <param name="administrador">Administrador a ser removido</param>
    void Apagar(Administrador administrador);
}

