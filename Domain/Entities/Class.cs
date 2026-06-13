namespace RpgBuilderMvc.Domain.Entities;

public class Class
{
    public int Id { get; set; }

    public string Index { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int HitDie { get; set; }

    public string Proficiencies { get; set; } = "";     
    public string SavingThrows { get; set; } = "";      
    public string Subclasses { get; set; } = "";
    public string StartingEquipment { get; set; } = "";    
}