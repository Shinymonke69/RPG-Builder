using System.ComponentModel.DataAnnotations;

namespace RpgBuilderMvc.Domain.Entities;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O E-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "O Nome de Aventureiro é obrigatório.")]
    [MinLength(3, ErrorMessage = "O Nome de Aventureiro deve ter no mínimo 3 caracteres.")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "A Palavra Secreta é obrigatória.")]
    public string PasswordHash { get; set; } = null!;

    public ICollection<Character> Characters { get; set; } = new List<Character>();
}
