using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.Domain.Entities;
using RpgBuilderMvc.Infrastructure.Persistence;
using RpgBuilderMvc.Application.ViewModels;
using RpgBuilderMvc.Services;

namespace RpgBuilderMvc.Controllers;

public class CharactersController(RpgDbContext db) : Controller
{
    private readonly Random random = new();

    // GET: /Characters
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Home");
    }

    // GET: /Characters/Create
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

        // escolhe um background aleatório para começar
        var randomBgIndex = backgrounds.Count > 0
            ? backgrounds[random.Next(backgrounds.Count)].Index
            : "";

        ViewBag.RandomBackground = randomBgIndex;

        // monta texto de traços do background selecionado
        var selectedBg = backgrounds.FirstOrDefault(b => b.Index == randomBgIndex);

        if (selectedBg != null)
        {
            // aqui usamos os campos que você preencheu no Program.cs [web:246][web:247]
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

        // monta texto de traços do background gerado
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

    // POST: /Characters/Create
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
    int currentHp = 0
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

        var character = new Character
        {
            Name = name,
            Level = level == 0 ? 1 : level,
            Xp = xp,
            CurrentHp = currentHp,
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

        int RollStat() => random.Next(8, 19); // 8–18

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

    // POST: /Characters/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var character = await db.Characters.FindAsync(id);
        if (character == null) return NotFound();

        var classes = await db.Classes.OrderBy(c => c.Name).ToListAsync();
        var races = await db.Races.OrderBy(r => r.Name).ToListAsync();
        var backgrounds = await db.Backgrounds.OrderBy(b => b.Name).ToListAsync();

        ViewBag.Classes = classes;
        ViewBag.Races = races;
        ViewBag.Backgrounds = backgrounds;

        // se você quiser continuar com ViewModel:
        var vm = new CharacterEditViewModel
        {
            Character = character,
            // se quiser, também pode por as listas aqui:
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
                .ToListAsync()
        };

        return View(vm);
    }

    // POST: /Characters/Edit/5
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
    int strength,
    int dexterity,
    int constitution,
    int intelligence,
    int wisdom,
    int charisma,
    int[] proficientSkills)
    {
        var character = await db.Characters.FindAsync(id);
        if (character == null) return NotFound();

        character.Name = name;
        character.Level = level;
        character.Xp = xp;
        character.CurrentHp = currentHp;
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

        // novos: salvar atributos
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

        await db.SaveChangesAsync();

        return RedirectToAction(nameof(Edit), new { id = character.Id });
    }

    // POST: /Characters/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var character = await db.Characters.FindAsync(id);
        if (character == null) return NotFound();

        db.Characters.Remove(character);
        await db.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }
}