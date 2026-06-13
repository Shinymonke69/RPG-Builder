namespace RpgBuilderMvc.DndApi.Models;

public class ApiListResponse
{
    public int Count { get; set; }
    public List<ApiReferenceDto> Results { get; set; } = [];
}

public class EquipmentListResponse
{
    public int Count { get; set; }
    public List<EquipmentListItem> Results { get; set; } = [];
}

public class EquipmentListItem
{
    public string Index { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
}