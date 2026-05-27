namespace RpgBuilderMvc.Domain.Entities;
public class Race
{
    public int Id { get; set; }
    public string Index { get; set; } = null!;
    public string Name { get; set; } = null!;
    public double Speed { get; set; }
    public string AbilityBonuses { get; set; } = "";
    public string Languages { get; set; } = "";
    public string Traits { get; set; } = "";
}