using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.Domain.Entities;
using RpgBuilderMvc.Infrastructure.Persistence;
using RpgBuilderMvc.Application.ViewModels;
using RpgBuilderMvc.Services;

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RpgBuilderMvc.Controllers;

[Authorize]
public class CharactersController(RpgDbContext db) : Controller
{
    private readonly Random random = new();

    
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Home");
    }

    
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var classes = await db.Classes.OrderBy(c => c.Name).ToListAsync();
        var races = await db.Races.OrderBy(r => r.Name).ToListAsync();
        var backgrounds = await db.Backgrounds.OrderBy(b => b.Name).ToListAsync();

        ViewBag.Classes = classes;
        ViewBag.Races = races;
        ViewBag.Backgrounds = backgrounds;
        ViewBag.RandomName = NameGenerator.GenerateFullName();

        var standardArray = new[] { 15, 14, 13, 12, 10, 8 };
        ViewBag.DefaultStats = standardArray.OrderBy(x => random.Next()).ToArray();

        var randomClassIndex = classes.Count > 0
            ? classes[random.Next(classes.Count)].Index
            : "";
        ViewBag.RandomClass = randomClassIndex;

        var randomRaceIndex = races.Count > 0
            ? races[random.Next(races.Count)].Index
            : "";
        ViewBag.RandomRace = randomRaceIndex;

        
        var randomBgIndex = backgrounds.Count > 0
            ? backgrounds[random.Next(backgrounds.Count)].Index
            : "";

        ViewBag.RandomBackground = randomBgIndex;

        
        var selectedBg = backgrounds.FirstOrDefault(b => b.Index == randomBgIndex);

        if (selectedBg != null)
        {
            
            var parts = new List<string>();

            if (!string.IsNullOrWhiteSpace(selectedBg.Feature))
                parts.Add($"Característica: {selectedBg.Feature}");

            if (!string.IsNullOrWhiteSpace(selectedBg.Skills))
                parts.Add($"Perícias: {selectedBg.Skills}");

            if (!string.IsNullOrWhiteSpace(selectedBg.Languages))
                parts.Add($"Idiomas: {selectedBg.Languages}");

            if (!string.IsNullOrWhiteSpace(selectedBg.Tools))
                parts.Add($"Ferramentas: {selectedBg.Tools}");

            if (!string.IsNullOrWhiteSpace(selectedBg.PersonalityTraits))
                parts.Add($"Traços de personalidade: {selectedBg.PersonalityTraits}");

            if (!string.IsNullOrWhiteSpace(selectedBg.Ideals))
                parts.Add($"Ideais: {selectedBg.Ideals}");

            if (!string.IsNullOrWhiteSpace(selectedBg.Bonds))
                parts.Add($"Laços: {selectedBg.Bonds}");

            if (!string.IsNullOrWhiteSpace(selectedBg.Flaws))
                parts.Add($"Defeitos: {selectedBg.Flaws}");

            ViewBag.BackgroundTraitsText = string.Join("\n", parts);
        }
        else
        {
            ViewBag.BackgroundTraitsText = "";
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GenerateRandom()
    {
        var classes = await db.Classes.OrderBy(c => c.Name).ToListAsync();
        var races = await db.Races.OrderBy(r => r.Name).ToListAsync();
        var backgrounds = await db.Backgrounds.OrderBy(b => b.Name).ToListAsync();

        ViewBag.Classes = classes;
        ViewBag.Races = races;
        ViewBag.Backgrounds = backgrounds;

        var randomCharacter = await GenerateRandomCharacterAsync();
        ViewBag.RandomCharacter = randomCharacter;

        var standardArray = new[] { 15, 14, 13, 12, 10, 8 };
        ViewBag.DefaultStats = standardArray.OrderBy(x => random.Next()).ToArray();

        
        var selectedBg = backgrounds.FirstOrDefault(b => b.Index == randomCharacter.BackgroundIndex);
        if (selectedBg != null)
        {
            var parts = new List<string>();

            if (!string.IsNullOrWhiteSpace(selectedBg.Feature))
                parts.Add($"Característica: {selectedBg.Feature}");

            if (!string.IsNullOrWhiteSpace(selectedBg.Skills))
                parts.Add($"Perícias: {selectedBg.Skills}");

            if (!string.IsNullOrWhiteSpace(selectedBg.Languages))
                parts.Add($"Idiomas: {selectedBg.Languages}");

            if (!string.IsNullOrWhiteSpace(selectedBg.Tools))
                parts.Add($"Ferramentas: {selectedBg.Tools}");

            if (!string.IsNullOrWhiteSpace(selectedBg.PersonalityTraits))
                parts.Add($"Traços de personalidade: {selectedBg.PersonalityTraits}");

            if (!string.IsNullOrWhiteSpace(selectedBg.Ideals))
                parts.Add($"Ideais: {selectedBg.Ideals}");

            if (!string.IsNullOrWhiteSpace(selectedBg.Bonds))
                parts.Add($"Laços: {selectedBg.Bonds}");

            if (!string.IsNullOrWhiteSpace(selectedBg.Flaws))
                parts.Add($"Defeitos: {selectedBg.Flaws}");

            ViewBag.BackgroundTraitsText = string.Join("\n", parts);
        }
        else
        {
            ViewBag.BackgroundTraitsText = "";
        }

        return View("Create");
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
    string name,
    int level,
    string classIndex,
    string raceIndex,
    string backgroundIndex,
    int strength,
    int dexterity,
    int constitution,
    int intelligence,
    int wisdom,
    int charisma,
    string? story,
    string? personalityTraits,
    string? ideals,
    string? bonds,
    string? flaws,
    string? equipmentNotes,
    int xp = 0,
    int currentHp = 0,
    int temporaryHp = 0
)
    {
        var cls = await db.Classes.FirstOrDefaultAsync(c => c.Index == classIndex);
        var race = await db.Races.FirstOrDefaultAsync(r => r.Index == raceIndex);
        var bg = await db.Backgrounds.FirstOrDefaultAsync(b => b.Index == backgroundIndex);

        if (cls == null || race == null || bg == null)
        {
            var classes = await db.Classes.OrderBy(c => c.Name).ToListAsync();
            var races = await db.Races.OrderBy(r => r.Name).ToListAsync();
            var backgrounds = await db.Backgrounds.OrderBy(b => b.Name).ToListAsync();

            ViewBag.Classes = classes;
            ViewBag.Races = races;
            ViewBag.Backgrounds = backgrounds;

            ModelState.AddModelError("", "Classe, raça ou antecedente inválido.");
            return View();
        }

        var hitDiceMap = new Dictionary<string, int> {
            { "barbarian", 12 }, { "bard", 8 }, { "warlock", 8 }, { "cleric", 8 },
            { "druid", 8 }, { "sorcerer", 6 }, { "fighter", 10 }, { "rogue", 8 },
            { "wizard", 6 }, { "monk", 8 }, { "paladin", 10 }, { "ranger", 10 }
        };
        int hd = hitDiceMap.GetValueOrDefault(cls.Index, 8);
        int conMod = (int)Math.Floor((constitution - 10) / 2.0);
        
        int finalHp = currentHp;
        if (finalHp == 0) 
        {
            int lvl = level == 0 ? 1 : level;
            int avgHitDie = (int)Math.Floor(hd / 2.0) + 1;
            finalHp = (hd + conMod) + (lvl - 1) * Math.Max(1, avgHitDie + conMod);
        }

        var character = new Character
        {
            UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!),
            Name = name,
            Level = level == 0 ? 1 : level,
            Xp = xp,
            CurrentHp = finalHp,
            TemporaryHp = temporaryHp,
            Status = "Ativo",

            ClassIndex = cls.Index,
            ClassName = cls.Name,
            RaceIndex = race.Index,
            RaceName = race.Name,
            BackgroundIndex = bg.Index,
            BackgroundName = bg.Name,

            Strength = strength,
            Dexterity = dexterity,
            Constitution = constitution,
            Intelligence = intelligence,
            Wisdom = wisdom,
            Charisma = charisma,

            Story = story ?? "",
            PersonalityTraits = personalityTraits ?? "",
            Ideals = ideals ?? "",
            Bonds = bonds ?? "",
            Flaws = flaws ?? "",
            EquipmentNotes = equipmentNotes ?? ""
        };

        db.Characters.Add(character);
        await db.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }
    private async Task<Character> GenerateRandomCharacterAsync()
    {
        var classes = await db.Classes.ToListAsync();
        var races = await db.Races.ToListAsync();
        var backgrounds = await db.Backgrounds.ToListAsync();

        if (classes.Count == 0 || races.Count == 0 || backgrounds.Count == 0)
            throw new InvalidOperationException("É preciso ter classes, raças e antecedentes cadastrados.");

        var cls = classes[random.Next(classes.Count)];
        var race = races[random.Next(races.Count)];
        var bg = backgrounds[random.Next(backgrounds.Count)];

        int RollStat() => random.Next(8, 19); 

        string fullName = NameGenerator.GenerateFullName();

        var character = new Character
        {
            Name = fullName,
            Level = random.Next(1, 21),
            Status = "Ativo",

            ClassIndex = cls.Index,
            ClassName = cls.Name,
            RaceIndex = race.Index,
            RaceName = race.Name,
            BackgroundIndex = bg.Index,
            BackgroundName = bg.Name,

            Strength = RollStat(),
            Dexterity = RollStat(),
            Constitution = RollStat(),
            Intelligence = RollStat(),
            Wisdom = RollStat(),
            Charisma = RollStat(),

            Story = "",
            PersonalityTraits = "",
            Ideals = "",
            Bonds = "",
            Flaws = "",
            EquipmentNotes = ""
        };

        return character;
    }

    
    public async Task<IActionResult> Edit(int id)
    {
        var character = await db.Characters.FindAsync(id);
        if (character == null) return NotFound();
        
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (character.UserId != userId) return Forbid();

        var classes = await db.Classes.OrderBy(c => c.Name).ToListAsync();
        var races = await db.Races.OrderBy(r => r.Name).ToListAsync();
        var backgrounds = await db.Backgrounds.OrderBy(b => b.Name).ToListAsync();

        ViewBag.Classes = classes;
        ViewBag.Races = races;
        ViewBag.Backgrounds = backgrounds;

        
        var vm = new CharacterEditViewModel
        {
            Character = character,
            
            Classes = classes,
            Races = races,
            Backgrounds = backgrounds,
            AllSkills = await db.Skills.OrderBy(s => s.Name).ToListAsync(),
            CharacterSkills = await db.CharacterSkills
                .Include(cs => cs.Skill)
                .Where(cs => cs.CharacterId == id)
                .ToListAsync(),
            AllSpells = await db.Spells
                .OrderBy(s => s.Level).ThenBy(s => s.Name).ToListAsync(),
            CharacterSpells = await db.CharacterSpells
                .Include(cs => cs.Spell)
                .Where(cs => cs.CharacterId == id)
                .ToListAsync(),
            AllWeapons = await db.Weapons.OrderBy(w => w.Name).ToListAsync(),
            CharacterWeapons = await db.CharacterWeapons
                .Include(cw => cw.Weapon)
                .Where(cw => cw.CharacterId == id)
                .ToListAsync(),
            AllArmors = await db.Armors.OrderBy(a => a.Name).ToListAsync(),
            CharacterArmors = await db.CharacterArmors
                .Include(ca => ca.Armor)
                .Where(ca => ca.CharacterId == id)
                .ToListAsync()
        };

        return View(vm);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
    int id,
    string name,
    int level,
    string status,
    string classIndex,
    string raceIndex,
    string backgroundIndex,
    string story,
    string personalityTraits,
    string ideals,
    string bonds,
    string flaws,
    string equipmentNotes,
    int xp,
    int currentHp,
    int temporaryHp,
    int strength,
    int dexterity,
    int constitution,
    int intelligence,
    int wisdom,
    int charisma,
    int[] proficientSkills,
    int[]? selectedSpells,
    int[]? selectedWeapons,
    int[]? selectedArmors)
    {
        var character = await db.Characters.FindAsync(id);
        if (character == null) return NotFound();
        
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (character.UserId != userId) return Forbid();

        character.Name = name;
        character.Level = level;
        character.Xp = xp;
        character.CurrentHp = currentHp;
        character.TemporaryHp = temporaryHp;
        character.Status = status ?? "Ativo";

        var cls = await db.Classes.FirstOrDefaultAsync(c => c.Index == classIndex);
        if (cls != null)
        {
            character.ClassIndex = cls.Index;
            character.ClassName = cls.Name;
        }

        var race = await db.Races.FirstOrDefaultAsync(r => r.Index == raceIndex);
        if (race != null)
        {
            character.RaceIndex = race.Index;
            character.RaceName = race.Name;
        }

        var bg = await db.Backgrounds.FirstOrDefaultAsync(b => b.Index == backgroundIndex);
        if (bg != null)
        {
            character.BackgroundIndex = bg.Index;
            character.BackgroundName = bg.Name;
        }

        
        character.Strength = strength;
        character.Dexterity = dexterity;
        character.Constitution = constitution;
        character.Intelligence = intelligence;
        character.Wisdom = wisdom;
        character.Charisma = charisma;

        character.Story = story ?? "";
        character.PersonalityTraits = personalityTraits ?? "";
        character.Ideals = ideals ?? "";
        character.Bonds = bonds ?? "";
        character.Flaws = flaws ?? "";
        character.EquipmentNotes = equipmentNotes ?? "";

        var existingSkills = await db.CharacterSkills.Where(cs => cs.CharacterId == id).ToListAsync();
        db.CharacterSkills.RemoveRange(existingSkills);

        
        var existingSpells = await db.CharacterSpells.Where(cs => cs.CharacterId == id).ToListAsync();
        db.CharacterSpells.RemoveRange(existingSpells);

        if (proficientSkills != null)
        {
            foreach (var skillId in proficientSkills)
            {
                db.CharacterSkills.Add(new CharacterSkill
                {
                    CharacterId = id,
                    SkillId = skillId,
                    IsProficient = true,
                    Bonus = 0 
                });
            }
        }

        if (selectedSpells != null)
        {
            foreach (var spellId in selectedSpells)
            {
                db.CharacterSpells.Add(new CharacterSpell
                {
                    CharacterId = id,
                    SpellId = spellId,
                    IsPrepared = false,
                    SlotLevel = 0
                });
            }
        }

        var existingWeapons = await db.CharacterWeapons.Where(cw => cw.CharacterId == id).ToListAsync();
        db.CharacterWeapons.RemoveRange(existingWeapons);

        if (selectedWeapons != null)
        {
            foreach (var weaponId in selectedWeapons)
            {
                db.CharacterWeapons.Add(new CharacterWeapon
                {
                    CharacterId = id,
                    WeaponId = weaponId,
                    IsEquipped = true
                });
            }
        }

        var existingArmors = await db.CharacterArmors.Where(ca => ca.CharacterId == id).ToListAsync();
        db.CharacterArmors.RemoveRange(existingArmors);

        if (selectedArmors != null)
        {
            foreach (var armorId in selectedArmors)
            {
                db.CharacterArmors.Add(new CharacterArmor
                {
                    CharacterId = id,
                    ArmorId = armorId,
                    IsEquipped = true
                });
            }
        }

        await db.SaveChangesAsync();

        return RedirectToAction(nameof(Edit), new { id = character.Id });
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var character = await db.Characters.FindAsync(id);
        if (character == null) return NotFound();
        
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (character.UserId != userId) return Forbid();

        db.Characters.Remove(character);
        await db.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }
}
