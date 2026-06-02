namespace RpgBuilderMvc.Domain.Entities;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Level { get; set; }
    public string Status { get; set; } = "Ativo";

    public string ClassIndex { get; set; } = null!;
    public string ClassName { get; set; } = null!;
    public string RaceIndex { get; set; } = null!;
    public string RaceName { get; set; } = null!;
    public string BackgroundIndex { get; set; } = null!;
    public string BackgroundName { get; set; } = null!;

    // Novos campos
    public string? Story { get; set; }
    public string? PersonalityTraits { get; set; }
    public string? Ideals { get; set; }
    public string? EquipmentNotes { get; set; }
}