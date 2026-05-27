namespace RpgBuilderMvc.Domain.Entities;

public class Spell
{
    public int Id { get; set; }

    public string Index { get; set; } = null!;
    public string Name { get; set; } = null!;

    public int Level { get; set; }

    public string School { get; set; } = "";

    public string CastingTime { get; set; } = "";
    public string Range { get; set; } = "";
    public string Components { get; set; } = "";      
    public string Material { get; set; } = "";

    public string Description { get; set; } = "";
}