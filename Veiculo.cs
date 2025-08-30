using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApi.Dominio.Entidades;

public class Veiculo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do veículo é obrigatório")]
    [StringLength(150, MinimumLength = 1, ErrorMessage = "O nome deve ter entre 1 e 150 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A marca do veículo é obrigatória")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "A marca deve ter entre 1 e 100 caracteres")]
    public string Marca { get; set; } = string.Empty;

    [Required(ErrorMessage = "O ano do veículo é obrigatório")]
    [Range(1950, 2030, ErrorMessage = "O ano deve estar entre 1950 e 2030")]
    public int Ano { get; set; }
}

