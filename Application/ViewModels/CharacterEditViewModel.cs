using RpgBuilderMvc.Domain.Entities;
using System.Collections.Generic;

namespace RpgBuilderMvc.Application.ViewModels
{
    public class CharacterEditViewModel
    {
        public Character Character { get; set; } = null!;

        
        public List<Class> Classes { get; set; } = [];
        public List<Race> Races { get; set; } = [];
        public List<Background> Backgrounds { get; set; } = [];

        
        public List<Skill> AllSkills { get; set; } = [];
        public List<CharacterSkill> CharacterSkills { get; set; } = [];

        
        public List<Spell> AllSpells { get; set; } = [];
        public List<CharacterSpell> CharacterSpells { get; set; } = [];

        
        public List<Weapon> AllWeapons { get; set; } = [];
        public List<CharacterWeapon> CharacterWeapons { get; set; } = [];
        public List<Armor> AllArmors { get; set; } = [];
        public List<CharacterArmor> CharacterArmors { get; set; } = [];
    }
}
