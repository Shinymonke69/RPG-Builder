namespace RpgBuilderMvc.DndApi.Models;

public class SpellDto
{
    public string Index { get; set; } = null!;
    public string Name { get; set; } = null!;

    public int Level { get; set; }


    public ApiReferenceDto? School { get; set; }

    public string Casting_Time { get; set; } = "";
    public string Range { get; set; } = "";
    public List<string> Components { get; set; } = [];
    public string? Material { get; set; }

    // Descrição principal
    public List<string> Desc { get; set; } = [];
}