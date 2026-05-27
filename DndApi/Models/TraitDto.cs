namespace RpgBuilderMvc.DndApi.Models;

public class TraitDto
{
    public string Index { get; set; } = null!;
    public string Name { get; set; } = null!;

    
    public List<string> Desc { get; set; } = [];
}