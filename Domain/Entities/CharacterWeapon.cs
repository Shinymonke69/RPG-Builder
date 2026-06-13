namespace RpgBuilderMvc.Domain.Entities;

public class CharacterWeapon
{
    public int Id { get; set; }

    public int CharacterId { get; set; }
    public Character Character { get; set; } = null!;

    public int WeaponId { get; set; }
    public Weapon Weapon { get; set; } = null!;

    public bool IsEquipped { get; set; }
}
