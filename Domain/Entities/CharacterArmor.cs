namespace RpgBuilderMvc.Domain.Entities;

public class CharacterArmor
{
    public int Id { get; set; }

    public int CharacterId { get; set; }
    public Character Character { get; set; } = null!;

    public int ArmorId { get; set; }
    public Armor Armor { get; set; } = null!;

    public bool IsEquipped { get; set; }
}
