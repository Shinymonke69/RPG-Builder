namespace RpgBuilderMvc.Domain.Entities;

public class Weapon
{
    public int Id { get; set; }
    public string Index { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Price { get; set; } = null!;
    public string Damage { get; set; } = "";
    public string Weight { get; set; } = "";
    public string Properties { get; set; } = "";
}