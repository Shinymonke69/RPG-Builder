using System.Linq;
using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.DndApi;
using RpgBuilderMvc.DndApi.Models;
using RpgBuilderMvc.Domain.Entities;
using RpgBuilderMvc.Infrastructure.Persistence;

namespace RpgBuilderMvc.Infrastructure.Sync;

public class SrdImporter(Dnd5eClient client, RpgDbContext db)
{

    public async Task ImportAllAsync()
    {
        await ImportClassesAsync();
        await ImportRacesAsync();
        await ImportTraitsAsync();
        await ImportSpellsAsync();

        await db.SaveChangesAsync();
    }

    private async Task ImportClassesAsync()
    {
        var list = await client.GetClassesAsync();

        foreach (var item in list.Results)
        {
            var dto = await client.GetClassAsync(item.Index);
            if (dto is null) continue;

            var entity = MapToClass(dto);

            var existing = await db.Classes
                .FirstOrDefaultAsync(c => c.Index == entity.Index);

            if (existing is null)
            {
                db.Classes.Add(entity);
            }
            else
            {
                existing.Name = entity.Name;
                existing.HitDie = entity.HitDie;
                existing.Proficiencies = entity.Proficiencies;
                existing.SavingThrows = entity.SavingThrows;
                existing.Subclasses = entity.Subclasses;
                existing.StartingEquipment = entity.StartingEquipment;
            }
        }
    }

    private static Class MapToClass(ClassDto dto)
    {
        var profs = dto.Proficiencies is null || dto.Proficiencies.Count == 0
            ? ""
            : string.Join(", ", dto.Proficiencies.Select(p => p.Name));

        var saves = dto.Saving_Throws is null || dto.Saving_Throws.Count == 0
            ? ""
            : string.Join(", ", dto.Saving_Throws.Select(p => p.Name));

        var subclasses = dto.Subclasses is null || dto.Subclasses.Count == 0
            ? ""
            : string.Join(", ", dto.Subclasses.Select(p => p.Name));

        var translatedName = ClassNameTranslations.TryGetValue(dto.Index, out var ptName)? ptName : dto.Name;

        return new Class
        {
            Index = dto.Index,
            Name = translatedName,
            HitDie = dto.Hit_Die,
            Proficiencies = profs,
            SavingThrows = saves,
            Subclasses = subclasses,
            StartingEquipment = "" 
        };
    }

    private static readonly Dictionary<string, string> ClassNameTranslations = new()
    {
        ["barbarian"] = "Bárbaro",
        ["bard"] = "Bardo",
        ["cleric"] = "Clérigo",
        ["druid"] = "Druida",
        ["fighter"] = "Guerreiro",
        ["monk"] = "Monge",
        ["paladin"] = "Paladino",
        ["ranger"] = "Patrulheiro",
        ["rogue"] = "Ladino",
        ["sorcerer"] = "Feiticeiro",
        ["warlock"] = "Bruxo",
        ["wizard"] = "Mago"
    };

    private async Task ImportRacesAsync()
    {
        var list = await client.GetRacesAsync();

        foreach (var item in list.Results)
        {
            var dto = await client.GetRaceAsync(item.Index);
            if (dto is null) continue;

            var entity = MapToRace(dto);

            var existing = await db.Races
                .FirstOrDefaultAsync(r => r.Index == entity.Index);

            if (existing is null)
            {
                db.Races.Add(entity);
            }
            else
            {
                existing.Name = entity.Name;
                existing.Speed = entity.Speed;
                existing.AbilityBonuses = entity.AbilityBonuses;
                existing.Languages = entity.Languages;
                existing.Traits = entity.Traits;
            }
        }
    }

    private static Race MapToRace(RaceDto dto)
    {
        var abilityBonuses = dto.Ability_Bonuses is null || dto.Ability_Bonuses.Count == 0
            ? ""
            : string.Join(", ", dto.Ability_Bonuses.Select(a => a.Name));

        var languages = dto.Languages is null || dto.Languages.Count == 0
            ? ""
            : string.Join(", ", dto.Languages.Select(l => l.Name));

        var traits = dto.Traits is null || dto.Traits.Count == 0
            ? ""
            : string.Join(", ", dto.Traits.Select(t => t.Name));

        return new Race
        {
            Index = dto.Index,
            Name = dto.Name,
            Speed = dto.Speed,  // agora é int direto
            AbilityBonuses = abilityBonuses,
            Languages = languages,
            Traits = traits
        };
    }
    private async Task ImportBackgroundsAsync()
    {
        var list = await client.GetBackgroundsAsync();

        foreach (var item in list.Results)
        {
            var dto = await client.GetBackgroundAsync(item.Index);
            if (dto is null) continue;

            var entity = MapToBackground(dto);

            var existing = await db.Backgrounds
                .FirstOrDefaultAsync(b => b.Index == entity.Index);

            if (existing is null)
            {
                db.Backgrounds.Add(entity);
            }
            else
            {
                existing.Name = entity.Name;
                existing.Feature = entity.Feature;
                existing.Skills = entity.Skills;
                existing.Languages = entity.Languages;
                existing.Tools = entity.Tools;
                existing.PersonalityTraits = entity.PersonalityTraits;
                existing.Ideals = entity.Ideals;
                existing.Bonds = entity.Bonds;
                existing.Flaws = entity.Flaws;
            }
        }
    }

    private static Background MapToBackground(BackgroundDto dto)
    {
        var featureStr = "";
        if (dto.Feature != null)
        {
            featureStr = $"**{dto.Feature.Name}**\n\n";
            if (dto.Feature.Desc != null && dto.Feature.Desc.Count > 0)
            {
                featureStr += string.Join("\n\n", dto.Feature.Desc);
            }
        }

        return new Background
        {
            Index = dto.Index,
            Name = dto.Name,
            Feature = featureStr,
            Skills = "",             
            Languages = string.Join(", ", dto.Languages.Select(l => l.Name)),
            Tools = "",              
            PersonalityTraits = "",  
            Ideals = "",
            Bonds = "",
            Flaws = ""
        };
    }


    private async Task ImportTraitsAsync()
    {
        var list = await client.GetTraitsAsync();

        foreach (var item in list.Results)
        {
            var dto = await client.GetTraitAsync(item.Index);
            if (dto is null) continue;

            var entity = MapToTrait(dto);

            var existing = await db.Traits
                .FirstOrDefaultAsync(t => t.Index == entity.Index);

            if (existing is null)
            {
                db.Traits.Add(entity);
            }
            else
            {
                existing.Name = entity.Name;
                existing.Description = entity.Description;
            }
        }
    }

    private static Trait MapToTrait(TraitDto dto)
    {
        var desc = dto.Desc is null || dto.Desc.Count == 0
            ? ""
            : string.Join("\n\n", dto.Desc);

        return new Trait
        {
            Index = dto.Index,
            Name = dto.Name,
            Description = desc
        };
    }


    private async Task ImportSpellsAsync()
    {
        var list = await client.GetSpellsAsync();

        foreach (var item in list.Results)
        {
            var dto = await client.GetSpellAsync(item.Index);
            if (dto is null) continue;

            var entity = MapToSpell(dto);

            var existing = await db.Spells
                .FirstOrDefaultAsync(s => s.Index == entity.Index);

            if (existing is null)
            {
                db.Spells.Add(entity);
            }
            else
            {
                existing.Name = entity.Name;
                existing.Level = entity.Level;
                existing.School = entity.School;
                existing.CastingTime = entity.CastingTime;
                existing.Range = entity.Range;
                existing.Components = entity.Components;
                existing.Material = entity.Material;
                existing.Description = entity.Description;
            }
        }
    }

    private static Spell MapToSpell(SpellDto dto)
    {
        var schoolName = dto.School?.Name ?? "";

        var components = dto.Components is null || dto.Components.Count == 0
            ? ""
            : string.Join(", ", dto.Components);

        var desc = dto.Desc is null || dto.Desc.Count == 0
            ? ""
            : string.Join("\n\n", dto.Desc);

        return new Spell
        {
            Index = dto.Index,
            Name = dto.Name,
            Level = dto.Level,
            School = schoolName,
            CastingTime = dto.Casting_Time,
            Range = dto.Range,
            Components = components,
            Material = dto.Material ?? "",
            Description = desc
        };
    }
}