using System.Security.Cryptography;
using System.Text;
using MinimalApi.Dominio.Interfaces;

namespace MinimalApi.Dominio.Servicos;

public class CriptografiaServico : ICriptografiaServico
{
    public string GerarHash(string senha)
    {
        if (string.IsNullOrEmpty(senha))
            throw new ArgumentException("Senha não pode ser nula ou vazia", nameof(senha));

        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        return Convert.ToBase64String(hashedBytes);
    }

    public bool VerificarSenha(string senha, string hash)
    {
        if (string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(hash))
            return false;

        var senhaHash = GerarHash(senha);
        return senhaHash == hash;
    }
}

