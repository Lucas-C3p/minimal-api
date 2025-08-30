using MinimalApi.Dominio.Entidades;
using MinimalApi.DTOs;
using MinimalApi.Infraestrutura.Db;
using MinimalApi.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.Dominio.Servicos;

public class AdministradorServico : IAdministradorServico
{
    private readonly DbContexto _contexto;
    private readonly ICriptografiaServico _criptografiaServico;
    
    public AdministradorServico(DbContexto contexto, ICriptografiaServico criptografiaServico)
    {
        _contexto = contexto;
        _criptografiaServico = criptografiaServico;
    }

    public Administrador? BuscaPorId(int id)
    {
        return _contexto.Administradores.FirstOrDefault(a => a.Id == id);
    }

    public Administrador Incluir(Administrador administrador)
    {
        // Criptografar a senha antes de salvar
        administrador.Senha = _criptografiaServico.GerarHash(administrador.Senha);
        
        _contexto.Administradores.Add(administrador);
        _contexto.SaveChanges();
        return administrador;
    }

    public Administrador? Login(LoginDTO loginDTO)
    {
        var administrador = _contexto.Administradores.FirstOrDefault(a => a.Email == loginDTO.Email);
        
        if (administrador != null && _criptografiaServico.VerificarSenha(loginDTO.Senha, administrador.Senha))
        {
            return administrador;
        }
        
        return null;
    }

    public List<Administrador> Todos(int? pagina = 1, int tamanhoPagina = 10)
    {
        var query = _contexto.Administradores.AsQueryable();

        if (pagina != null && pagina > 0)
        {
            query = query.Skip(((int)pagina - 1) * tamanhoPagina).Take(tamanhoPagina);
        }

        return query.ToList();
    }

    public void Atualizar(Administrador administrador)
    {
        var administradorExistente = _contexto.Administradores.FirstOrDefault(a => a.Id == administrador.Id);
        if (administradorExistente != null)
        {
            administradorExistente.Email = administrador.Email;
            administradorExistente.Perfil = administrador.Perfil;
            
            // Só atualizar a senha se ela foi alterada
            if (!string.IsNullOrEmpty(administrador.Senha) && 
                !_criptografiaServico.VerificarSenha(administrador.Senha, administradorExistente.Senha))
            {
                administradorExistente.Senha = _criptografiaServico.GerarHash(administrador.Senha);
            }
            
            _contexto.SaveChanges();
        }
    }

    public void Apagar(Administrador administrador)
    {
        _contexto.Administradores.Remove(administrador);
        _contexto.SaveChanges();
    }
}

