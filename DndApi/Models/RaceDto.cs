namespace RpgBuilderMvc.DndApi.Models;

public class RaceDto
{
    public string Index { get; set; } = null!;
    public string Name { get; set; } = null!;
    public double Speed { get; set; }
    public List<ApiReferenceDto> Ability_Bonuses { get; set; } = [];
    public List<ApiReferenceDto> Languages { get; set; } = [];
    public List<ApiReferenceDto> Traits { get; set; } = [];
}