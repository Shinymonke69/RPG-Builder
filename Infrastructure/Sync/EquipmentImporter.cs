using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.DndApi;
using RpgBuilderMvc.DndApi.Models;
using RpgBuilderMvc.Domain.Entities;
using RpgBuilderMvc.Infrastructure.Persistence;

namespace RpgBuilderMvc.Infrastructure.Sync;

public class EquipmentImporter
{
    private readonly Dnd5eClient _client;
    private readonly RpgDbContext _db;

    public EquipmentImporter(Dnd5eClient client, RpgDbContext db)
    {
        _client = client;
        _db = db;
    }

    public async Task ImportAsync()
    {
        var list = await _client.GetEquipmentListAsync();

        foreach (var item in list.Results)
        {
            var dto = await _client.GetEquipmentAsync(item.Index);
            if (dto is null) continue;

            if (dto.Weapon_Category is not null)
            {
                var weapon = MapToWeapon(dto);
                if (!await _db.Weapons.AnyAsync(w => w.Index == weapon.Index))
                    _db.Weapons.Add(weapon);
            }
            else if (dto.Armor_Category is not null)
            {
                var armor = MapToArmor(dto);
                if (!await _db.Armors.AnyAsync(a => a.Index == armor.Index))
                    _db.Armors.Add(armor);
            }
        }

        await _db.SaveChangesAsync();
    }

    private Weapon MapToWeapon(EquipmentDto dto)
    {
        var price = dto.Cost is null ? "" : $"{dto.Cost.Quantity} {dto.Cost.Unit}";

        var props = dto.Properties is null || dto.Properties.Count == 0
            ? ""
            : string.Join(", ", dto.Properties.Select(p => p.Name));

        return new Weapon
        {
            Index = dto.Index,
            Name = dto.Name,
            Price = price,
            Damage = dto.Damage?.Damage_Dice ?? "",
            Weight = dto.Weight?.ToString() ?? "",
            Properties = props
        };
    }

    private Armor MapToArmor(EquipmentDto dto)
    {
        var price = dto.Cost is null ? "" : $"{dto.Cost.Quantity} {dto.Cost.Unit}";

        return new Armor
        {
            Index = dto.Index,
            Name = dto.Name,
            Price = price,
            // Por enquanto deixamos esses campos vazios; depois mapeamos certinho
            Ac = "",
            Strength = "",
            Stealth = "",
            Weight = dto.Weight?.ToString() ?? ""
        };
    }
}