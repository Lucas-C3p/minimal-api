using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Infraestrutura.Db;

namespace Test.Domain.Servicos;

[TestClass]
public class VeiculoServicoTest
{
    private DbContexto CriarContextoTeste()
    {
        var options = new DbContextOptionsBuilder<DbContexto>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new DbContexto(options);
    }

    [TestMethod]
    public void TestarSalvarVeiculo()
    {
        // Arrange
        var context = CriarContextoTeste();
        var veiculoServico = new VeiculoServico(context);
        var veiculo = new Veiculo
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2022
        };

        // Act
        veiculoServico.Incluir(veiculo);

        // Assert
        Assert.AreEqual(1, veiculo.Id);
        var veiculoSalvo = veiculoServico.BuscaPorId(1);
        Assert.IsNotNull(veiculoSalvo);
        Assert.AreEqual("Civic", veiculoSalvo.Nome);
        Assert.AreEqual("Honda", veiculoSalvo.Marca);
        Assert.AreEqual(2022, veiculoSalvo.Ano);
    }

    [TestMethod]
    public void TestarBuscarVeiculoPorId()
    {
        // Arrange
        var context = CriarContextoTeste();
        var veiculoServico = new VeiculoServico(context);
        var veiculo = new Veiculo
        {
            Nome = "Corolla",
            Marca = "Toyota",
            Ano = 2023
        };
        veiculoServico.Incluir(veiculo);

        // Act
        var veiculoEncontrado = veiculoServico.BuscaPorId(veiculo.Id);

        // Assert
        Assert.IsNotNull(veiculoEncontrado);
        Assert.AreEqual(veiculo.Id, veiculoEncontrado.Id);
        Assert.AreEqual("Corolla", veiculoEncontrado.Nome);
    }

    [TestMethod]
    public void TestarBuscarVeiculoInexistente()
    {
        // Arrange
        var context = CriarContextoTeste();
        var veiculoServico = new VeiculoServico(context);

        // Act
        var veiculo = veiculoServico.BuscaPorId(999);

        // Assert
        Assert.IsNull(veiculo);
    }

    [TestMethod]
    public void TestarListarTodosVeiculos()
    {
        // Arrange
        var context = CriarContextoTeste();
        var veiculoServico = new VeiculoServico(context);
        
        veiculoServico.Incluir(new Veiculo { Nome = "Civic", Marca = "Honda", Ano = 2022 });
        veiculoServico.Incluir(new Veiculo { Nome = "Corolla", Marca = "Toyota", Ano = 2023 });

        // Act
        var veiculos = veiculoServico.Todos();

        // Assert
        Assert.AreEqual(2, veiculos.Count);
    }

    [TestMethod]
    public void TestarAtualizarVeiculo()
    {
        // Arrange
        var context = CriarContextoTeste();
        var veiculoServico = new VeiculoServico(context);
        var veiculo = new Veiculo
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2022
        };
        veiculoServico.Incluir(veiculo);

        // Act
        veiculo.Nome = "Civic Atualizado";
        veiculo.Ano = 2024;
        veiculoServico.Atualizar(veiculo);

        // Assert
        var veiculoAtualizado = veiculoServico.BuscaPorId(veiculo.Id);
        Assert.IsNotNull(veiculoAtualizado);
        Assert.AreEqual("Civic Atualizado", veiculoAtualizado.Nome);
        Assert.AreEqual(2024, veiculoAtualizado.Ano);
    }

    [TestMethod]
    public void TestarApagarVeiculo()
    {
        // Arrange
        var context = CriarContextoTeste();
        var veiculoServico = new VeiculoServico(context);
        var veiculo = new Veiculo
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2022
        };
        veiculoServico.Incluir(veiculo);

        // Act
        veiculoServico.Apagar(veiculo);

        // Assert
        var veiculoApagado = veiculoServico.BuscaPorId(veiculo.Id);
        Assert.IsNull(veiculoApagado);
    }

    [TestMethod]
    public void TestarFiltrarVeiculosPorNome()
    {
        // Arrange
        var context = CriarContextoTeste();
        var veiculoServico = new VeiculoServico(context);
        
        veiculoServico.Incluir(new Veiculo { Nome = "Civic", Marca = "Honda", Ano = 2022 });
        veiculoServico.Incluir(new Veiculo { Nome = "Corolla", Marca = "Toyota", Ano = 2023 });
        veiculoServico.Incluir(new Veiculo { Nome = "City", Marca = "Honda", Ano = 2021 });

        // Act
        var veiculos = veiculoServico.Todos(nome: "Civ");

        // Assert
        Assert.AreEqual(1, veiculos.Count);
        Assert.AreEqual("Civic", veiculos.First().Nome);
    }

    [TestMethod]
    public void TestarFiltrarVeiculosPorMarca()
    {
        // Arrange
        var context = CriarContextoTeste();
        var veiculoServico = new VeiculoServico(context);
        
        veiculoServico.Incluir(new Veiculo { Nome = "Civic", Marca = "Honda", Ano = 2022 });
        veiculoServico.Incluir(new Veiculo { Nome = "Corolla", Marca = "Toyota", Ano = 2023 });
        veiculoServico.Incluir(new Veiculo { Nome = "City", Marca = "Honda", Ano = 2021 });

        // Act
        var veiculos = veiculoServico.Todos(marca: "Honda");

        // Assert
        Assert.AreEqual(2, veiculos.Count);
        Assert.IsTrue(veiculos.All(v => v.Marca == "Honda"));
    }
}

