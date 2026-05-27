namespace RpgBuilderMvc.DndApi.Models;

public class ClassDto
{
    public string Index { get; set; } = null!;
    public string Name { get; set; } = null!;

    public int Hit_Die { get; set; }

    public List<ApiReferenceDto> Proficiencies { get; set; } = [];
    public List<ApiReferenceDto> Saving_Throws { get; set; } = [];
    public List<ApiReferenceDto> Subclasses { get; set; } = [];
}