#pragma warning disable CS8604
#pragma warning disable CS8602

using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Dominio.ModelViews;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Dominio.Validadores;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Value ?? 
             builder.Configuration.GetSection("Jwt").Value ?? 
             "minimal-api-default-key-32-chars-min";

builder.Services.AddAuthentication(option => {
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option => {
    option.TokenValidationParameters = new TokenValidationParameters{
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.AddAuthorization();

builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();
builder.Services.AddScoped<ITokenServico, TokenServico>();
builder.Services.AddScoped<ICriptografiaServico, CriptografiaServico>();
builder.Services.AddScoped<ValidacaoServico>();

// Registrar validadores FluentValidation
builder.Services.AddScoped<IValidator<VeiculoDTO>, VeiculoDTOValidator>();
builder.Services.AddScoped<IValidator<AdministradorDTO>, AdministradorDTOValidator>();
builder.Services.AddScoped<IValidator<LoginDTO>, LoginDTOValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minimal API - Sistema de Veículos",
        Version = "v1.0",
        Description = "API para gerenciamento de veículos com autenticação JWT",
        Contact = new OpenApiContact
        {
            Name = "Equipe de Desenvolvimento",
            Email = "dev@minimalapi.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Configuração para autenticação JWT no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu_token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Configurações adicionais para melhor documentação
    // options.EnableAnnotations(); // Não disponível no .NET 7
    // options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MinimalApi.xml"), true); // Comentários XML não configurados
});
// Configuração do banco de dados
var databaseProvider = builder.Configuration.GetValue<string>("DatabaseProvider") ?? "Sqlite";

switch (databaseProvider.ToLower())
{
    case "mysql":
        builder.Services.AddDbContext<DbContexto>(options => {
            options.UseMySql(
                builder.Configuration.GetConnectionString("MySql"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySql"))
            );
        });
        break;
    case "sqlite":
    default:
        builder.Services.AddDbContext<DbContexto>(options => {
            options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
        });
        break;
}

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Garantir que o banco de dados seja criado
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DbContexto>();
    context.Database.EnsureCreated();
}
#endregion

#region Configure
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API v1.0");
        c.RoutePrefix = string.Empty; // Swagger na raiz da aplicação
        c.DocumentTitle = "Minimal API - Sistema de Veículos";
        c.DefaultModelsExpandDepth(-1); // Ocultar modelos por padrão
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
        c.EnableFilter();
        c.ShowExtensions();
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API v1.0");
        c.RoutePrefix = "swagger";
    });
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

#region Home
app.MapGet("/", () => Results.Json(new Home()))
.AllowAnonymous()
.WithTags("Home")
.WithSummary("Página inicial da API")
.WithDescription("Retorna informações básicas sobre a API de veículos")
.Produces<Home>(200, "application/json");
#endregion

#region Administradores
app.MapPost("/administradores/login", ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico, ITokenServico tokenServico, ValidacaoServico validacaoServico) => {
    // Validação usando FluentValidation
    var errosValidacao = validacaoServico.Validar(loginDTO);
    if (errosValidacao != null)
        return Results.BadRequest(errosValidacao);

    var adm = administradorServico.Login(loginDTO);
    if(adm != null)
    {
        string token = tokenServico.GerarToken(adm);
        return Results.Ok(new AdministradorLogado
        {
            Email = adm.Email,
            Perfil = adm.Perfil,
            Token = token
        });
    }
    else
        return Results.Unauthorized();
})
.AllowAnonymous()
.WithTags("Administradores")
.WithSummary("Realizar login de administrador")
.WithDescription("Autentica um administrador e retorna um token JWT para acesso aos endpoints protegidos")
.Accepts<LoginDTO>("application/json")
.Produces<AdministradorLogado>(200)
.Produces(401);

app.MapGet("/administradores", ([FromQuery] int? pagina, [FromQuery] int? tamanhoPagina, IAdministradorServico administradorServico) => {
    var adms = new List<AdministradorModelView>();
    var administradores = administradorServico.Todos(pagina, tamanhoPagina ?? 10);
    foreach(var adm in administradores)
    {
        adms.Add(new AdministradorModelView{
            Id = adm.Id,
            Email = adm.Email,
            Perfil = adm.Perfil
        });
    }
    return Results.Ok(adms);
})
.RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
.WithTags("Administradores")
.WithSummary("Listar administradores")
.WithDescription("Lista todos os administradores cadastrados no sistema com paginação. Requer perfil de Administrador.")
.Produces<List<AdministradorModelView>>(200)
.Produces(401)
.Produces(403);

app.MapGet("/administradores/{id}", ([FromRoute] int id, IAdministradorServico administradorServico) => {
    if (id <= 0)
        return Results.BadRequest(new ErrosDeValidacao { Mensagens = new List<string> { "ID deve ser maior que zero" } });

    var administrador = administradorServico.BuscaPorId(id);
    if(administrador == null) 
        return Results.NotFound(new { Mensagem = "Administrador não encontrado" });
    
    return Results.Ok(new AdministradorModelView {
        Id = administrador.Id,
        Email = administrador.Email,
        Perfil = administrador.Perfil
    });
})
.RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
.WithTags("Administradores")
.WithSummary("Buscar administrador por ID")
.WithDescription("Busca um administrador específico pelo seu ID. Requer perfil de Administrador.")
.Produces<AdministradorModelView>(200)
.Produces(400)
.Produces(401)
.Produces(403)
.Produces(404);

app.MapPost("/administradores", ([FromBody] AdministradorDTO administradorDTO, IAdministradorServico administradorServico, ValidacaoServico validacaoServico) => {
    // Validação usando FluentValidation
    var errosValidacao = validacaoServico.Validar(administradorDTO);
    if (errosValidacao != null)
        return Results.BadRequest(errosValidacao);
    
    var administrador = new Administrador{
        Email = administradorDTO.Email,
        Senha = administradorDTO.Senha,
        Perfil = administradorDTO.Perfil.ToString() ?? Perfil.Editor.ToString()
    };
    
    administradorServico.Incluir(administrador);

    return Results.Created($"/administradores/{administrador.Id}", new AdministradorModelView {
        Id = administrador.Id,
        Email = administrador.Email,
        Perfil = administrador.Perfil
    });
})
.RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
.WithTags("Administradores")
.WithSummary("Criar novo administrador")
.WithDescription("Cria um novo administrador no sistema. Requer perfil de Administrador.")
.Accepts<AdministradorDTO>("application/json")
.Produces<AdministradorModelView>(201)
.Produces<ErrosDeValidacao>(400)
.Produces(401)
.Produces(403);
#endregion

#region Veiculos
app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico, ValidacaoServico validacaoServico) => {
    // Validação usando FluentValidation
    var errosValidacao = validacaoServico.Validar(veiculoDTO);
    if (errosValidacao != null)
        return Results.BadRequest(errosValidacao);
    
    var veiculo = new Veiculo{
        Nome = veiculoDTO.Nome,
        Marca = veiculoDTO.Marca,
        Ano = veiculoDTO.Ano
    };
    
    veiculoServico.Incluir(veiculo);

    return Results.Created($"/veiculos/{veiculo.Id}", veiculo);
})
.RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" })
.WithTags("Veiculos")
.WithSummary("Criar um novo veículo")
.WithDescription("Cria um novo veículo no sistema. Requer autenticação e perfil de Administrador ou Editor.");

app.MapGet("/veiculos", ([FromQuery] int? pagina, [FromQuery] int? tamanhoPagina, [FromQuery] string? nome, [FromQuery] string? marca, IVeiculoServico veiculoServico) => {
    var veiculos = veiculoServico.Todos(pagina, tamanhoPagina ?? 10, nome, marca);
    return Results.Ok(veiculos);
})
.RequireAuthorization()
.WithTags("Veiculos")
.WithSummary("Listar veículos")
.WithDescription("Lista todos os veículos com filtros opcionais por nome e marca, e suporte à paginação.");

app.MapGet("/veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) => {
    if (id <= 0)
        return Results.BadRequest(new ErrosDeValidacao { Mensagens = new List<string> { "ID deve ser maior que zero" } });

    var veiculo = veiculoServico.BuscaPorId(id);
    if(veiculo == null) 
        return Results.NotFound(new { Mensagem = "Veículo não encontrado" });
    
    return Results.Ok(veiculo);
})
.RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" })
.WithTags("Veiculos")
.WithSummary("Buscar veículo por ID")
.WithDescription("Busca um veículo específico pelo seu ID.");

app.MapPut("/veiculos/{id}", ([FromRoute] int id, [FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico, ValidacaoServico validacaoServico) => {
    if (id <= 0)
        return Results.BadRequest(new ErrosDeValidacao { Mensagens = new List<string> { "ID deve ser maior que zero" } });

    var veiculo = veiculoServico.BuscaPorId(id);
    if(veiculo == null) 
        return Results.NotFound(new { Mensagem = "Veículo não encontrado" });
    
    // Validação usando FluentValidation
    var errosValidacao = validacaoServico.Validar(veiculoDTO);
    if (errosValidacao != null)
        return Results.BadRequest(errosValidacao);
    
    veiculo.Nome = veiculoDTO.Nome;
    veiculo.Marca = veiculoDTO.Marca;
    veiculo.Ano = veiculoDTO.Ano;

    veiculoServico.Atualizar(veiculo);

    return Results.Ok(veiculo);
})
.RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
.WithTags("Veiculos")
.WithSummary("Atualizar veículo")
.WithDescription("Atualiza os dados de um veículo existente. Requer perfil de Administrador.");

app.MapDelete("/veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) => {
    if (id <= 0)
        return Results.BadRequest(new ErrosDeValidacao { Mensagens = new List<string> { "ID deve ser maior que zero" } });

    var veiculo = veiculoServico.BuscaPorId(id);
    if(veiculo == null) 
        return Results.NotFound(new { Mensagem = "Veículo não encontrado" });

    veiculoServico.Apagar(veiculo);

    return Results.NoContent();
})
.RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
.WithTags("Veiculos")
.WithSummary("Excluir veículo")
.WithDescription("Remove um veículo do sistema. Requer perfil de Administrador.");
#endregion

#endregion

app.Run();

// Torna a classe Program acessível para os testes
public partial class Program { }

#pragma warning restore CS8604
#pragma warning restore CS8602
