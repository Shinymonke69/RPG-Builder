using RpgBuilderMvc.Domain.Entities;
using System.Collections.Generic;

namespace RpgBuilderMvc.Application.ViewModels
{
    public class CharacterEditViewModel
    {
        public Character Character { get; set; } = null!;

        // Para selects de classe/raça/background
        public List<Class> Classes { get; set; } = [];
        public List<Race> Races { get; set; } = [];
        public List<Background> Backgrounds { get; set; } = [];

        // Skills
        public List<Skill> AllSkills { get; set; } = [];
        public List<CharacterSkill> CharacterSkills { get; set; } = [];

        // Spells
        public List<Spell> AllSpells { get; set; } = [];
        public List<CharacterSpell> CharacterSpells { get; set; } = [];
    }
}