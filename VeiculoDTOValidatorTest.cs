using MinimalApi.Dominio.Validadores;
using MinimalApi.DTOs;

namespace Test.Domain.Validadores;

[TestClass]
public class VeiculoDTOValidatorTest
{
    private readonly VeiculoDTOValidator _validator;

    public VeiculoDTOValidatorTest()
    {
        _validator = new VeiculoDTOValidator();
    }

    [TestMethod]
    public void TestarVeiculoValido()
    {
        // Arrange
        var veiculo = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2022
        };

        // Act
        var resultado = _validator.Validate(veiculo);

        // Assert
        Assert.IsTrue(resultado.IsValid);
        Assert.AreEqual(0, resultado.Errors.Count);
    }

    [TestMethod]
    public void TestarNomeVazio()
    {
        // Arrange
        var veiculo = new VeiculoDTO
        {
            Nome = "",
            Marca = "Honda",
            Ano = 2022
        };

        // Act
        var resultado = _validator.Validate(veiculo);

        // Assert
        Assert.IsFalse(resultado.IsValid);
        Assert.IsTrue(resultado.Errors.Any(e => e.ErrorMessage.Contains("nome do veículo é obrigatório")));
    }

    [TestMethod]
    public void TestarNomeMuitoCurto()
    {
        // Arrange
        var veiculo = new VeiculoDTO
        {
            Nome = "A",
            Marca = "Honda",
            Ano = 2022
        };

        // Act
        var resultado = _validator.Validate(veiculo);

        // Assert
        Assert.IsFalse(resultado.IsValid);
        Assert.IsTrue(resultado.Errors.Any(e => e.ErrorMessage.Contains("entre 2 e 150 caracteres")));
    }

    [TestMethod]
    public void TestarNomeComCaracteresInvalidos()
    {
        // Arrange
        var veiculo = new VeiculoDTO
        {
            Nome = "Civic@#$",
            Marca = "Honda",
            Ano = 2022
        };

        // Act
        var resultado = _validator.Validate(veiculo);

        // Assert
        Assert.IsFalse(resultado.IsValid);
        Assert.IsTrue(resultado.Errors.Any(e => e.ErrorMessage.Contains("apenas letras, números, espaços e hífens")));
    }

    [TestMethod]
    public void TestarMarcaVazia()
    {
        // Arrange
        var veiculo = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "",
            Ano = 2022
        };

        // Act
        var resultado = _validator.Validate(veiculo);

        // Assert
        Assert.IsFalse(resultado.IsValid);
        Assert.IsTrue(resultado.Errors.Any(e => e.ErrorMessage.Contains("marca do veículo é obrigatória")));
    }

    [TestMethod]
    public void TestarAnoMuitoAntigo()
    {
        // Arrange
        var veiculo = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 1949
        };

        // Act
        var resultado = _validator.Validate(veiculo);

        // Assert
        Assert.IsFalse(resultado.IsValid);
        Assert.IsTrue(resultado.Errors.Any(e => e.ErrorMessage.Contains("igual ou superior a 1950")));
    }

    [TestMethod]
    public void TestarAnoFuturoInvalido()
    {
        // Arrange
        var anoFuturo = DateTime.Now.Year + 2;
        var veiculo = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = anoFuturo
        };

        // Act
        var resultado = _validator.Validate(veiculo);

        // Assert
        Assert.IsFalse(resultado.IsValid);
        Assert.IsTrue(resultado.Errors.Any(e => e.ErrorMessage.Contains("não pode ser superior")));
    }

    [TestMethod]
    public void TestarAnoAtualValido()
    {
        // Arrange
        var veiculo = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = DateTime.Now.Year
        };

        // Act
        var resultado = _validator.Validate(veiculo);

        // Assert
        Assert.IsTrue(resultado.IsValid);
    }

    [TestMethod]
    public void TestarAnoProximoAnoValido()
    {
        // Arrange
        var veiculo = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = DateTime.Now.Year + 1
        };

        // Act
        var resultado = _validator.Validate(veiculo);

        // Assert
        Assert.IsTrue(resultado.IsValid);
    }
}

