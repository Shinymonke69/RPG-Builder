using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.Domain.Entities;
using RpgBuilderMvc.Infrastructure.Persistence;

namespace RpgBuilderMvc.Controllers;

public class CharactersController(RpgDbContext db) : Controller
{

    // GET: /Characters
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Home");
    }

    // GET: /Characters/Create
    public async Task<IActionResult> Create()
    {
        var classes = await db.Classes.OrderBy(c => c.Name).ToListAsync();
        var races = await db.Races.OrderBy(r => r.Name).ToListAsync();
        var backgrounds = await db.Backgrounds.OrderBy(b => b.Name).ToListAsync();

        ViewBag.Classes = classes;
        ViewBag.Races = races;
        ViewBag.Backgrounds = backgrounds;

        return View();
    }

    // POST: /Characters/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
    string name,
    int level,
    string classIndex,
    string raceIndex,
    string backgroundIndex)
    {
        var cls = await db.Classes.FirstOrDefaultAsync(c => c.Index == classIndex);
        var race = await db.Races.FirstOrDefaultAsync(r => r.Index == raceIndex);
        var bg = await db.Backgrounds.FirstOrDefaultAsync(b => b.Index == backgroundIndex);

        if (cls is null || race is null || bg is null)
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
            Level = level,
            Status = "Ativo",
            ClassIndex = cls.Index,
            ClassName = cls.Name,
            RaceIndex = race.Index,
            RaceName = race.Name,
            BackgroundIndex = bg.Index,
            BackgroundName = bg.Name,

            Story = "",
            PersonalityTraits = "",
            Ideals = "",
            EquipmentNotes = ""
        };

        db.Characters.Add(character);
        await db.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

    // GET: /Characters/Edit/5
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

        return View(character);
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
    string? story,
    string? personalityTraits,
    string? ideals,
    string? equipmentNotes)
    {
        var character = await db.Characters.FindAsync(id);
        if (character == null) return NotFound();

        var cls = await db.Classes.FirstOrDefaultAsync(c => c.Index == classIndex);
        var race = await db.Races.FirstOrDefaultAsync(r => r.Index == raceIndex);
        var bg = await db.Backgrounds.FirstOrDefaultAsync(b => b.Index == backgroundIndex);

        if (cls == null || race == null || bg == null)
        {
            ModelState.AddModelError("", "Classe, raça ou antecedente inválido.");

            var classes = await db.Classes.OrderBy(c => c.Name).ToListAsync();
            var races = await db.Races.OrderBy(r => r.Name).ToListAsync();
            var backgrounds = await db.Backgrounds.OrderBy(b => b.Name).ToListAsync();

            ViewBag.Classes = classes;
            ViewBag.Races = races;
            ViewBag.Backgrounds = backgrounds;

            return View(character);
        }

        character.Name = name;
        character.Level = level;
        character.Status = status;
        character.ClassIndex = cls.Index;
        character.ClassName = cls.Name;
        character.RaceIndex = race.Index;
        character.RaceName = race.Name;
        character.BackgroundIndex = bg.Index;
        character.BackgroundName = bg.Name;

        character.Story = story;
        character.PersonalityTraits = personalityTraits;
        character.Ideals = ideals;
        character.EquipmentNotes = equipmentNotes;

        await db.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
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