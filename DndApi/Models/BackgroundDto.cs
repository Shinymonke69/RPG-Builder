namespace RpgBuilderMvc.DndApi.Models;

public class BackgroundDto
{
    public string Index { get; set; } = null!;
    public string Name { get; set; } = null!;

    public BackgroundFeatureDto? Feature { get; set; }
    public List<ApiReferenceDto> Languages { get; set; } = [];
    public List<ApiReferenceDto> Skills { get; set; } = [];
}

public class BackgroundFeatureDto
{
    public string Name { get; set; } = "";
    public List<string> Desc { get; set; } = [];
}