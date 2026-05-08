using System.Net.Http.Json;
using RpgBuilderMvc.DndApi.Models;

namespace RpgBuilderMvc.DndApi;

public class Dnd5eClient
{
    private readonly HttpClient _http;

    public Dnd5eClient(HttpClient httpClient)
    {
        _http = httpClient;
        _http.BaseAddress = new Uri("https://www.dnd5eapi.co");
    }

    public async Task<EquipmentListResponse> GetEquipmentListAsync()
    {
        var response = await _http.GetFromJsonAsync<EquipmentListResponse>("/api/equipment");
        return response ?? new EquipmentListResponse();
    }

    public async Task<EquipmentDto?> GetEquipmentAsync(string index)
    {
        // Exemplo: /api/2014/equipment/longsword
        return await _http.GetFromJsonAsync<EquipmentDto>($"/api/2014/equipment/{index}");
    }
}