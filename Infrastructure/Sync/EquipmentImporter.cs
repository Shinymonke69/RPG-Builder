using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.DndApi;
using RpgBuilderMvc.DndApi.Models;
using RpgBuilderMvc.Domain.Entities;
using RpgBuilderMvc.Infrastructure.Persistence;

namespace RpgBuilderMvc.Infrastructure.Sync;

public class EquipmentImporter(Dnd5eClient client, RpgDbContext db)
{
    public async Task ImportAsync()
    {
        var list = await client.GetEquipmentListAsync();

        foreach (var item in list.Results)
        {
            var dto = await client.GetEquipmentAsync(item.Index);
            if (dto is null) continue;

            if (dto.Weapon_Category is not null)
            {
                var weapon = MapToWeapon(dto);

                var existing = await db.Weapons
                    .FirstOrDefaultAsync(w => w.Index == weapon.Index);

                if (existing is null)
                {
                    db.Weapons.Add(weapon);
                }
                else
                {
                    existing.Name = weapon.Name;
                    existing.Price = weapon.Price;
                    existing.Damage = weapon.Damage;
                    existing.Weight = weapon.Weight;
                    existing.Properties = weapon.Properties;

                }
            }
            else if (dto.Armor_Category is not null)
            {
                var armor = MapToArmor(dto);

                
                var existing = await db.Armors
                    .FirstOrDefaultAsync(a => a.Index == armor.Index);

                if (existing is null)
                {
                    db.Armors.Add(armor);
                }
                else
                {
                    existing.Name = armor.Name;
                    existing.Price = armor.Price;
                    existing.Ac = armor.Ac;
                    existing.Strength = armor.Strength;
                    existing.Stealth = armor.Stealth;
                    existing.Weight = armor.Weight;

                }
            }
        }

        await db.SaveChangesAsync();
    }

    private static Weapon MapToWeapon(EquipmentDto dto)
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

    private static Armor MapToArmor(EquipmentDto dto)
    {
        var price = dto.Cost is null ? "" : $"{dto.Cost.Quantity} {dto.Cost.Unit}";

        return new Armor
        {
            Index = dto.Index,
            Name = dto.Name,
            Price = price,
            Ac = "",
            Strength = "",
            Stealth = "",
            Weight = dto.Weight?.ToString() ?? ""
        };
    }
}