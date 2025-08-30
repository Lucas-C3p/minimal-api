using MinimalApi.Dominio.Servicos;

namespace Test.Domain.Servicos;

[TestClass]
public class CriptografiaServicoTest
{
    private readonly CriptografiaServico _criptografiaServico;

    public CriptografiaServicoTest()
    {
        _criptografiaServico = new CriptografiaServico();
    }

    [TestMethod]
    public void TestarGerarHash()
    {
        // Arrange
        var senha = "123456";

        // Act
        var hash = _criptografiaServico.GerarHash(senha);

        // Assert
        Assert.IsNotNull(hash);
        Assert.IsTrue(hash.Length > 0);
        Assert.AreNotEqual(senha, hash);
    }

    [TestMethod]
    public void TestarGerarHashSenhasIguais()
    {
        // Arrange
        var senha = "123456";

        // Act
        var hash1 = _criptografiaServico.GerarHash(senha);
        var hash2 = _criptografiaServico.GerarHash(senha);

        // Assert
        Assert.AreEqual(hash1, hash2);
    }

    [TestMethod]
    public void TestarGerarHashSenhasDiferentes()
    {
        // Arrange
        var senha1 = "123456";
        var senha2 = "654321";

        // Act
        var hash1 = _criptografiaServico.GerarHash(senha1);
        var hash2 = _criptografiaServico.GerarHash(senha2);

        // Assert
        Assert.AreNotEqual(hash1, hash2);
    }

    [TestMethod]
    public void TestarVerificarSenhaCorreta()
    {
        // Arrange
        var senha = "123456";
        var hash = _criptografiaServico.GerarHash(senha);

        // Act
        var resultado = _criptografiaServico.VerificarSenha(senha, hash);

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    public void TestarVerificarSenhaIncorreta()
    {
        // Arrange
        var senhaCorreta = "123456";
        var senhaIncorreta = "654321";
        var hash = _criptografiaServico.GerarHash(senhaCorreta);

        // Act
        var resultado = _criptografiaServico.VerificarSenha(senhaIncorreta, hash);

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestarGerarHashSenhaVazia()
    {
        // Arrange & Act & Assert
        _criptografiaServico.GerarHash("");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestarGerarHashSenhaNula()
    {
        // Arrange & Act & Assert
        _criptografiaServico.GerarHash(null!);
    }

    [TestMethod]
    public void TestarVerificarSenhaVazia()
    {
        // Arrange
        var hash = _criptografiaServico.GerarHash("123456");

        // Act
        var resultado = _criptografiaServico.VerificarSenha("", hash);

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    public void TestarVerificarHashVazio()
    {
        // Arrange & Act
        var resultado = _criptografiaServico.VerificarSenha("123456", "");

        // Assert
        Assert.IsFalse(resultado);
    }
}

