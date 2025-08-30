using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;

namespace Test.Mocks;

public class VeiculoServicoMock : IVeiculoServico
{
    private static List<Veiculo> veiculos = new List<Veiculo>(){
        new Veiculo{
            Id = 1,
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2022
        },
        new Veiculo{
            Id = 2,
            Nome = "Corolla",
            Marca = "Toyota",
            Ano = 2023
        },
        new Veiculo{
            Id = 3,
            Nome = "Onix",
            Marca = "Chevrolet",
            Ano = 2021
        }
    };

    public Veiculo? BuscaPorId(int id)
    {
        return veiculos.Find(v => v.Id == id);
    }

    public void Incluir(Veiculo veiculo)
    {
        veiculo.Id = veiculos.Count() + 1;
        veiculos.Add(veiculo);
    }

    public List<Veiculo> Todos(int? pagina = 1, int tamanhoPagina = 10, string? nome = null, string? marca = null)
    {
        var query = veiculos.AsQueryable();

        if (!string.IsNullOrEmpty(nome))
        {
            query = query.Where(v => v.Nome.ToLower().Contains(nome.ToLower()));
        }

        if (!string.IsNullOrEmpty(marca))
        {
            query = query.Where(v => v.Marca.ToLower().Contains(marca.ToLower()));
        }

        return query.ToList();
    }

    public void Atualizar(Veiculo veiculo)
    {
        var index = veiculos.FindIndex(v => v.Id == veiculo.Id);
        if (index >= 0)
        {
            veiculos[index] = veiculo;
        }
    }

    public void Apagar(Veiculo veiculo)
    {
        veiculos.RemoveAll(v => v.Id == veiculo.Id);
    }
}

