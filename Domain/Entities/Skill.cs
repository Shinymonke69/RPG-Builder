namespace RpgBuilderMvc.Domain.Entities;

public class Skill
{
    public int Id { get; set; }
    public string Index { get; set; } = null!;   
    public string Name { get; set; } = null!;
    public string Ability { get; set; } = null!;
}

public class CharacterSkill
{
    public int Id { get; set; }

    public int CharacterId { get; set; }
    public Character Character { get; set; } = null!;

    public int SkillId { get; set; }
    public Skill Skill { get; set; } = null!;

    public bool IsProficient { get; set; }
    public bool IsExpert { get; set; }
    public int Bonus { get; set; } 
}