using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Infraestrutura.Db;
using Test.Mocks;

namespace Test.Helpers;

public class Setup
{
    public const string PORT = "5001";
    public static TestContext testContext = default!;
    public static WebApplicationFactory<Program> http = default!;
    public static HttpClient client = default!;

    public static void ClassInit(TestContext testContext)
    {
        Setup.testContext = testContext;
        Setup.http = new WebApplicationFactory<Program>();

        Setup.http = Setup.http.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("https_port", Setup.PORT).UseEnvironment("Testing");
            
            builder.ConfigureServices(services =>
            {
                // Remove o DbContext padrão e adiciona um InMemory para testes
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DbContexto>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<DbContexto>(options =>
                {
                    options.UseInMemoryDatabase("TestDatabase");
                });

                // Substitui os serviços por mocks quando necessário
                services.AddScoped<IAdministradorServico, AdministradorServicoMock>();
            });
        });

        Setup.client = Setup.http.CreateClient();
    }

    public static void ClassCleanup()
    {
        Setup.http.Dispose();
    }
}

