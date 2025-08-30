using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Servicos;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

namespace Test.Domain.Servicos;

[TestClass]
public class AdministradorServicoTest
{
    private DbContexto CriarContextoTeste()
    {
        var options = new DbContextOptionsBuilder<DbContexto>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new DbContexto(options);
    }

    [TestMethod]
    public void TestarSalvarAdministrador()
    {
        // Arrange
        var context = CriarContextoTeste();
        var criptografiaServico = new CriptografiaServico();
        var administradorServico = new AdministradorServico(context, criptografiaServico);
        
        var adm = new Administrador
        {
            Email = "teste@teste.com",
            Senha = "123456",
            Perfil = "Adm"
        };

        // Act
        administradorServico.Incluir(adm);

        // Assert
        Assert.AreEqual(1, adm.Id);
        var administradores = administradorServico.Todos();
        Assert.AreEqual(1, administradores.Count);
    }

    [TestMethod]
    public void TestarBuscarAdministradorPorId()
    {
        // Arrange
        var context = CriarContextoTeste();
        var criptografiaServico = new CriptografiaServico();
        var administradorServico = new AdministradorServico(context, criptografiaServico);
        
        var adm = new Administrador
        {
            Email = "teste@teste.com",
            Senha = "123456",
            Perfil = "Adm"
        };
        
        administradorServico.Incluir(adm);

        // Act
        var admEncontrado = administradorServico.BuscaPorId(adm.Id);

        // Assert
        Assert.IsNotNull(admEncontrado);
        Assert.AreEqual(adm.Id, admEncontrado.Id);
        Assert.AreEqual("teste@teste.com", admEncontrado.Email);
    }

    [TestMethod]
    public void TestarBuscarAdministradorInexistente()
    {
        // Arrange
        var context = CriarContextoTeste();
        var criptografiaServico = new CriptografiaServico();
        var administradorServico = new AdministradorServico(context, criptografiaServico);

        // Act
        var adm = administradorServico.BuscaPorId(999);

        // Assert
        Assert.IsNull(adm);
    }

    [TestMethod]
    public void TestarLoginComSucesso()
    {
        // Arrange
        var context = CriarContextoTeste();
        var criptografiaServico = new CriptografiaServico();
        var administradorServico = new AdministradorServico(context, criptografiaServico);
        
        var adm = new Administrador
        {
            Email = "teste@teste.com",
            Senha = "123456",
            Perfil = "Adm"
        };
        
        administradorServico.Incluir(adm);

        var loginDTO = new LoginDTO
        {
            Email = "teste@teste.com",
            Senha = "123456"
        };

        // Act
        var admLogado = administradorServico.Login(loginDTO);

        // Assert
        Assert.IsNotNull(admLogado);
        Assert.AreEqual("teste@teste.com", admLogado.Email);
    }

    [TestMethod]
    public void TestarLoginComSenhaIncorreta()
    {
        // Arrange
        var context = CriarContextoTeste();
        var criptografiaServico = new CriptografiaServico();
        var administradorServico = new AdministradorServico(context, criptografiaServico);
        
        var adm = new Administrador
        {
            Email = "teste@teste.com",
            Senha = "123456",
            Perfil = "Adm"
        };
        
        administradorServico.Incluir(adm);

        var loginDTO = new LoginDTO
        {
            Email = "teste@teste.com",
            Senha = "senhaerrada"
        };

        // Act
        var admLogado = administradorServico.Login(loginDTO);

        // Assert
        Assert.IsNull(admLogado);
    }

    [TestMethod]
    public void TestarListarTodosAdministradores()
    {
        // Arrange
        var context = CriarContextoTeste();
        var criptografiaServico = new CriptografiaServico();
        var administradorServico = new AdministradorServico(context, criptografiaServico);
        
        administradorServico.Incluir(new Administrador { Email = "adm1@teste.com", Senha = "123456", Perfil = "Adm" });
        administradorServico.Incluir(new Administrador { Email = "adm2@teste.com", Senha = "123456", Perfil = "Editor" });

        // Act
        var administradores = administradorServico.Todos();

        // Assert
        Assert.AreEqual(2, administradores.Count);
    }

    [TestMethod]
    public void TestarAtualizarAdministrador()
    {
        // Arrange
        var context = CriarContextoTeste();
        var criptografiaServico = new CriptografiaServico();
        var administradorServico = new AdministradorServico(context, criptografiaServico);
        
        var adm = new Administrador
        {
            Email = "teste@teste.com",
            Senha = "123456",
            Perfil = "Editor"
        };
        
        administradorServico.Incluir(adm);

        // Act
        adm.Email = "novoemail@teste.com";
        adm.Perfil = "Adm";
        administradorServico.Atualizar(adm);

        // Assert
        var admAtualizado = administradorServico.BuscaPorId(adm.Id);
        Assert.IsNotNull(admAtualizado);
        Assert.AreEqual("novoemail@teste.com", admAtualizado.Email);
        Assert.AreEqual("Adm", admAtualizado.Perfil);
    }

    [TestMethod]
    public void TestarApagarAdministrador()
    {
        // Arrange
        var context = CriarContextoTeste();
        var criptografiaServico = new CriptografiaServico();
        var administradorServico = new AdministradorServico(context, criptografiaServico);
        
        var adm = new Administrador
        {
            Email = "teste@teste.com",
            Senha = "123456",
            Perfil = "Adm"
        };
        
        administradorServico.Incluir(adm);

        // Act
        administradorServico.Apagar(adm);

        // Assert
        var admApagado = administradorServico.BuscaPorId(adm.Id);
        Assert.IsNull(admApagado);
    }
}

