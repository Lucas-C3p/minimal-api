using System.ComponentModel.DataAnnotations;

namespace MinimalApi.DTOs;

public class LoginDTO
{
    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres")]
    public string Senha { get; set; } = string.Empty;
}

