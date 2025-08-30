namespace MinimalApi.Dominio.Interfaces;

public interface ICriptografiaServico
{
    /// <summary>
    /// Gera o hash de uma senha
    /// </summary>
    /// <param name="senha">Senha em texto plano</param>
    /// <returns>Hash da senha</returns>
    string GerarHash(string senha);

    /// <summary>
    /// Verifica se uma senha corresponde ao hash
    /// </summary>
    /// <param name="senha">Senha em texto plano</param>
    /// <param name="hash">Hash para comparação</param>
    /// <returns>True se a senha corresponder ao hash, false caso contrário</returns>
    bool VerificarSenha(string senha, string hash);
}

