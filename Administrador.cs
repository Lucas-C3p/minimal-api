using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApi.Dominio.Entidades;

public class Administrador
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "O email é obrigatório")]
    [StringLength(255, ErrorMessage = "O email deve ter no máximo 255 caracteres")]
    [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres")]
    public string Senha { get; set; } = string.Empty;

    [Required(ErrorMessage = "O perfil é obrigatório")]
    [StringLength(10, ErrorMessage = "O perfil deve ter no máximo 10 caracteres")]
    public string Perfil { get; set; } = string.Empty;
}

