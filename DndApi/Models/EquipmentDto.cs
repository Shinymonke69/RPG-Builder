namespace RpgBuilderMvc.DndApi.Models;

public class EquipmentDto
{
    public string Index { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Equipment_Category { get; set; } = null!;

    public string? Weapon_Category { get; set; }
    public string? Armor_Category { get; set; }

    public CostDto? Cost { get; set; }
    public double? Weight { get; set; }

    public DamageDto? Damage { get; set; }

    public List<EquipmentPropertyDto> Properties { get; set; } = [];
}

public class CostDto
{
    public int Quantity { get; set; }
    public string Unit { get; set; } = null!;
}

public class DamageDto
{
    public string Damage_Dice { get; set; } = null!;
    public DamageTypeDto Damage_Type { get; set; } = null!;
}

public class DamageTypeDto
{
    public string Index { get; set; } = null!;
    public string Name { get; set; } = null!;
}

public class EquipmentPropertyDto
{
    public string Index { get; set; } = null!;
    public string Name { get; set; } = null!;
}