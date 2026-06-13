namespace RpgBuilderMvc.Domain.Entities;

public class Armor
{
    public int Id { get; set; }
    public string Index { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Price { get; set; } = null!;
    public string Ac { get; set; } = null!;
    public string Strength { get; set; } = "";
    public string Stealth { get; set; } = "";
    public string Weight { get; set; } = "";
}