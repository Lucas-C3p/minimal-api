using System.ComponentModel.DataAnnotations;
using MinimalApi.Dominio.Enuns;

namespace MinimalApi.DTOs;

public class AdministradorDTO
{
    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
    [StringLength(255, ErrorMessage = "O email deve ter no máximo 255 caracteres")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres")]
    public string Senha { get; set; } = string.Empty;

    [Required(ErrorMessage = "O perfil é obrigatório")]
    public Perfil? Perfil { get; set; }
}

