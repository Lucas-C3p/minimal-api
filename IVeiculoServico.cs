using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Dominio.Interfaces;

public interface IVeiculoServico
{
    /// <summary>
    /// Lista todos os veículos com filtros opcionais e paginação
    /// </summary>
    /// <param name="pagina">Número da página (padrão: 1)</param>
    /// <param name="tamanhoPagina">Tamanho da página (padrão: 10)</param>
    /// <param name="nome">Filtro por nome do veículo</param>
    /// <param name="marca">Filtro por marca do veículo</param>
    /// <returns>Lista de veículos</returns>
    List<Veiculo> Todos(int? pagina = 1, int tamanhoPagina = 10, string? nome = null, string? marca = null);

    /// <summary>
    /// Busca um veículo pelo ID
    /// </summary>
    /// <param name="id">ID do veículo</param>
    /// <returns>Veículo encontrado ou null</returns>
    Veiculo? BuscaPorId(int id);

    /// <summary>
    /// Inclui um novo veículo no sistema
    /// </summary>
    /// <param name="veiculo">Dados do veículo a ser incluído</param>
    void Incluir(Veiculo veiculo);

    /// <summary>
    /// Atualiza os dados de um veículo
    /// </summary>
    /// <param name="veiculo">Veículo com dados atualizados</param>
    void Atualizar(Veiculo veiculo);

    /// <summary>
    /// Remove um veículo do sistema
    /// </summary>
    /// <param name="veiculo">Veículo a ser removido</param>
    void Apagar(Veiculo veiculo);
}

