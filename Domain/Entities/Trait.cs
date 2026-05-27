namespace RpgBuilderMvc.Domain.Entities;

public class Trait
{
    public int Id { get; set; }

    public string Index { get; set; } = null!;
    public string Name { get; set; } = null!;

    public string Description { get; set; } = "";
}