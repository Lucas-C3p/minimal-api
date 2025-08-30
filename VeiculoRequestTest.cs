using System.Net;
using System.Text;
using System.Text.Json;
using MinimalApi.DTOs;
using Test.Helpers;

namespace Test.Requests;

[TestClass]
public class VeiculoRequestTest
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
    public async Task TestarGetVeiculos()
    {
        // Arrange & Act
        var response = await Setup.client.GetAsync("/veiculos");

        // Assert
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarPostVeiculoSemAutenticacao()
    {
        // Arrange
        var veiculo = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2022
        };

        var json = JsonSerializer.Serialize(veiculo);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Setup.client.PostAsync("/veiculos", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarPostVeiculoComDadosInvalidos()
    {
        // Arrange
        var loginDTO = new LoginDTO
        {
            Email = "adm@teste.com",
            Senha = "123456"
        };

        var loginJson = JsonSerializer.Serialize(loginDTO);
        var loginContent = new StringContent(loginJson, Encoding.UTF8, "application/json");

        // Fazer login primeiro
        var loginResponse = await Setup.client.PostAsync("/administradores/login", loginContent);
        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

        var loginResult = await loginResponse.Content.ReadAsStringAsync();
        var loginData = JsonSerializer.Deserialize<JsonElement>(loginResult);
        var token = loginData.GetProperty("token").GetString();

        // Configurar autorização
        Setup.client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var veiculoInvalido = new VeiculoDTO
        {
            Nome = "", // Nome vazio - inválido
            Marca = "Honda",
            Ano = 2022
        };

        var json = JsonSerializer.Serialize(veiculoInvalido);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await Setup.client.PostAsync("/veiculos", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarGetVeiculoPorIdInexistente()
    {
        // Arrange
        var loginDTO = new LoginDTO
        {
            Email = "adm@teste.com",
            Senha = "123456"
        };

        var loginJson = JsonSerializer.Serialize(loginDTO);
        var loginContent = new StringContent(loginJson, Encoding.UTF8, "application/json");

        // Fazer login primeiro
        var loginResponse = await Setup.client.PostAsync("/administradores/login", loginContent);
        var loginResult = await loginResponse.Content.ReadAsStringAsync();
        var loginData = JsonSerializer.Deserialize<JsonElement>(loginResult);
        var token = loginData.GetProperty("token").GetString();

        Setup.client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await Setup.client.GetAsync("/veiculos/999");

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarGetVeiculoComIdInvalido()
    {
        // Arrange
        var loginDTO = new LoginDTO
        {
            Email = "adm@teste.com",
            Senha = "123456"
        };

        var loginJson = JsonSerializer.Serialize(loginDTO);
        var loginContent = new StringContent(loginJson, Encoding.UTF8, "application/json");

        var loginResponse = await Setup.client.PostAsync("/administradores/login", loginContent);
        var loginResult = await loginResponse.Content.ReadAsStringAsync();
        var loginData = JsonSerializer.Deserialize<JsonElement>(loginResult);
        var token = loginData.GetProperty("token").GetString();

        Setup.client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await Setup.client.GetAsync("/veiculos/0");

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarDeleteVeiculoSemPermissao()
    {
        // Arrange
        var loginDTO = new LoginDTO
        {
            Email = "editor@teste.com", // Editor não pode deletar
            Senha = "123456"
        };

        var loginJson = JsonSerializer.Serialize(loginDTO);
        var loginContent = new StringContent(loginJson, Encoding.UTF8, "application/json");

        var loginResponse = await Setup.client.PostAsync("/administradores/login", loginContent);
        var loginResult = await loginResponse.Content.ReadAsStringAsync();
        var loginData = JsonSerializer.Deserialize<JsonElement>(loginResult);
        var token = loginData.GetProperty("token").GetString();

        Setup.client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await Setup.client.DeleteAsync("/veiculos/1");

        // Assert
        Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
    }
}

