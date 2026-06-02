namespace RpgBuilderMvc.Domain.Entities;

public class CharacterSpell
{
    public int Id { get; set; }

    public int CharacterId { get; set; }
    public Character Character { get; set; } = null!;

    public int SpellId { get; set; }
    public Spell Spell { get; set; } = null!;

    public bool IsPrepared { get; set; }   
    public int SlotLevel { get; set; }     
}