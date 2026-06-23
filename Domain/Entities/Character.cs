using System.ComponentModel.DataAnnotations;

namespace RpgBuilderMvc.Domain.Entities;

public class Character
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [Required(ErrorMessage = "O Nome do personagem é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O Nome não pode exceder 100 caracteres.")]
    public string Name { get; set; } = null!;

    [Range(1, 20, ErrorMessage = "O Nível deve ser entre 1 e 20.")]
    public int Level { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "O XP não pode ser negativo.")]
    public int Xp { get; set; }
    
    public int CurrentHp { get; set; }
    public int TemporaryHp { get; set; }
    public string Status { get; set; } = "Ativo";

    public string ClassIndex { get; set; } = null!;
    public string ClassName { get; set; } = null!;
    public string RaceIndex { get; set; } = null!;
    public string RaceName { get; set; } = null!;
    public string BackgroundIndex { get; set; } = null!;
    public string BackgroundName { get; set; } = null!;

    [Range(1, 30, ErrorMessage = "Atributo inválido.")]
    public int Strength { get; set; }
    [Range(1, 30, ErrorMessage = "Atributo inválido.")]
    public int Dexterity { get; set; }
    [Range(1, 30, ErrorMessage = "Atributo inválido.")]
    public int Constitution { get; set; }
    [Range(1, 30, ErrorMessage = "Atributo inválido.")]
    public int Intelligence { get; set; }
    [Range(1, 30, ErrorMessage = "Atributo inválido.")]
    public int Wisdom { get; set; }
    [Range(1, 30, ErrorMessage = "Atributo inválido.")]
    public int Charisma { get; set; }

    public string? Story { get; set; }
    public string? PersonalityTraits { get; set; }
    public string? Ideals { get; set; }
    public string? Bonds { get; set; }
    public string? Flaws { get; set; }
    public string? EquipmentNotes { get; set; }
}
