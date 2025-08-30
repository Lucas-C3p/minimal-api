using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Enuns;

namespace MinimalApi.Infraestrutura.Db;

public class DbContexto : DbContext
{
    public DbContexto(DbContextOptions<DbContexto> options) : base(options)
    {
    }

    public DbSet<Administrador> Administradores { get; set; } = default!;
    public DbSet<Veiculo> Veiculos { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurações da entidade Administrador
        modelBuilder.Entity<Administrador>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Senha).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Perfil).IsRequired().HasMaxLength(10);
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Configurações da entidade Veiculo
        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Marca).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Ano).IsRequired();
        });

        // Seed de dados iniciais
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Administrador padrão com senha criptografada
        var criptografiaServico = new MinimalApi.Dominio.Servicos.CriptografiaServico();
        
        modelBuilder.Entity<Administrador>().HasData(
            new Administrador
            {
                Id = 1,
                Email = "admin@minimalapi.com",
                Senha = criptografiaServico.GerarHash("123456"), // Senha: 123456
                Perfil = Perfil.Adm.ToString()
            }
        );

        // Veículos de exemplo
        modelBuilder.Entity<Veiculo>().HasData(
            new Veiculo
            {
                Id = 1,
                Nome = "Civic",
                Marca = "Honda",
                Ano = 2022
            },
            new Veiculo
            {
                Id = 2,
                Nome = "Corolla",
                Marca = "Toyota",
                Ano = 2023
            },
            new Veiculo
            {
                Id = 3,
                Nome = "Onix",
                Marca = "Chevrolet",
                Ano = 2021
            }
        );
    }
}

