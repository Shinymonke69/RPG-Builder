namespace RpgBuilderMvc.Domain.Entities;

public class Character
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int Level { get; set; }
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

    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }
    public int Wisdom { get; set; }
    public int Charisma { get; set; }

    // Novos campos
    public string? Story { get; set; }
    public string? PersonalityTraits { get; set; }
    public string? Ideals { get; set; }
    public string? Bonds { get; set; }
    public string? Flaws { get; set; }
    public string? EquipmentNotes { get; set; }
}