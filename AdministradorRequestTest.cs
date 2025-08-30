using System.Net;
using System.Text;
using System.Text.Json;
using MinimalApi.Dominio.Enuns;
using MinimalApi.Dominio.ModelViews;
using MinimalApi.DTOs;
using Test.Helpers;

namespace Test.Requests;

[TestClass]
public class AdministradorRequestTest
{
    [ClassInitialize]
    public static void ClassInit(TestContext testContext)
    {
        Setup.ClassInit(testContext);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        Setup.ClassCleanup();
    }
    
    [TestMethod]
    public async Task TestarLoginComSucesso()
    {
        // Arrange
        var loginDTO = new LoginDTO{
            Email = "adm@teste.com",
            Senha = "123456"
        };

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8,  "Application/json");

        // Act
        var response = await Setup.client.PostAsync("/administradores/login", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var admLogado = JsonSerializer.Deserialize<AdministradorLogado>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(admLogado?.Email);
        Assert.IsNotNull(admLogado?.Perfil);
        Assert.IsNotNull(admLogado?.Token);
        Assert.AreEqual("adm@teste.com", admLogado.Email);
    }

    [TestMethod]
    public async Task TestarLoginComCredenciaisInvalidas()
    {
        // Arrange
        var loginDTO = new LoginDTO{
            Email = "adm@teste.com",
            Senha = "senhaerrada"
        };

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8,  "Application/json");

        // Act
        var response = await Setup.client.PostAsync("/administradores/login", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarLoginComEmailInvalido()
    {
        // Arrange
        var loginDTO = new LoginDTO{
            Email = "emailinvalido",
            Senha = "123456"
        };

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8,  "Application/json");

        // Act
        var response = await Setup.client.PostAsync("/administradores/login", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarLoginComDadosVazios()
    {
        // Arrange
        var loginDTO = new LoginDTO{
            Email = "",
            Senha = ""
        };

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8,  "Application/json");

        // Act
        var response = await Setup.client.PostAsync("/administradores/login", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarGetAdministradoresSemAutenticacao()
    {
        // Act
        var response = await Setup.client.GetAsync("/administradores");

        // Assert
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarCriarAdministradorComDadosInvalidos()
    {
        // Arrange
        var adminDTO = new AdministradorDTO{
            Email = "emailinvalido", // Email inválido
            Senha = "123", // Senha muito fraca
            Perfil = Perfil.Editor
        };

        var content = new StringContent(JsonSerializer.Serialize(adminDTO), Encoding.UTF8,  "Application/json");

        // Act
        var response = await Setup.client.PostAsync("/administradores", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode); // Sem autenticação primeiro
    }

    [TestMethod]
    public async Task TestarGetAdministradorPorIdInexistente()
    {
        // Arrange
        var loginDTO = new LoginDTO{
            Email = "adm@teste.com",
            Senha = "123456"
        };

        var loginContent = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8,  "Application/json");
        var loginResponse = await Setup.client.PostAsync("/administradores/login", loginContent);
        
        var loginResult = await loginResponse.Content.ReadAsStringAsync();
        var admLogado = JsonSerializer.Deserialize<AdministradorLogado>(loginResult, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Setup.client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", admLogado?.Token);

        // Act
        var response = await Setup.client.GetAsync("/administradores/999");

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarGetAdministradorComIdInvalido()
    {
        // Arrange
        var loginDTO = new LoginDTO{
            Email = "adm@teste.com",
            Senha = "123456"
        };

        var loginContent = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8,  "Application/json");
        var loginResponse = await Setup.client.PostAsync("/administradores/login", loginContent);
        
        var loginResult = await loginResponse.Content.ReadAsStringAsync();
        var admLogado = JsonSerializer.Deserialize<AdministradorLogado>(loginResult, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Setup.client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", admLogado?.Token);

        // Act
        var response = await Setup.client.GetAsync("/administradores/0");

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarAcessoEndpointHome()
    {
        // Act
        var response = await Setup.client.GetAsync("/");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        
        var result = await response.Content.ReadAsStringAsync();
        var home = JsonSerializer.Deserialize<Home>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(home);
        Assert.IsNotNull(home.Mensagem);
    }
}

