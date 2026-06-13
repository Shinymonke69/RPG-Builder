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

    public async Task<ApiListResponse> GetListAsync(string endpoint)
    {
        var response = await _http.GetFromJsonAsync<ApiListResponse>(
            $"/api/2014/{endpoint}?lang=pt-BR"
        );

        return response ?? new ApiListResponse();
    }

    public async Task<EquipmentListResponse> GetEquipmentListAsync()
    {
        var genericList = await GetListAsync("equipment");

        return new EquipmentListResponse
        {
            Count = genericList.Count,
            Results =
            [
                .. genericList.Results.Select(r => new EquipmentListItem
            {
                Index = r.Index,
                Name = r.Name,
                Url = r.Url
            })
            ]
        };
    }
    public async Task<EquipmentDto?> GetEquipmentAsync(string index)
    {
        return await _http.GetFromJsonAsync<EquipmentDto>(
            $"/api/2014/equipment/{index}?lang=pt-BR"
        );
    }

    public async Task<ApiListResponse> GetClassesAsync()
    {
        return await GetListAsync("classes");
    }

    public async Task<ClassDto?> GetClassAsync(string index)
    {
        return await _http.GetFromJsonAsync<ClassDto>(
            $"/api/2014/classes/{index}?lang=pt-BR"
        );
    }


    public async Task<ApiListResponse> GetRacesAsync()
    {
        return await GetListAsync("races");
    }

    public async Task<RaceDto?> GetRaceAsync(string index)
    {
        return await _http.GetFromJsonAsync<RaceDto>(
            $"/api/2014/races/{index}?lang=pt-BR"
        );
    }


    public async Task<ApiListResponse> GetBackgroundsAsync()
    {
        return await GetListAsync("backgrounds");
    }

    public async Task<BackgroundDto?> GetBackgroundAsync(string index)
    {
        return await _http.GetFromJsonAsync<BackgroundDto>(
            $"/api/2014/backgrounds/{index}?lang=pt-BR"
        );
    }


    public async Task<ApiListResponse> GetTraitsAsync()
    {
        return await GetListAsync("traits");
    }

    public async Task<TraitDto?> GetTraitAsync(string index)
    {
        return await _http.GetFromJsonAsync<TraitDto>(
            $"/api/2014/traits/{index}?lang=pt-BR"
        );
    }

    public async Task<ApiListResponse> GetSpellsAsync()
    {
        return await GetListAsync("spells");
    }

    public async Task<SpellDto?> GetSpellAsync(string index)
    {
        return await _http.GetFromJsonAsync<SpellDto>(
            $"/api/2014/spells/{index}?lang=pt-BR"
        );
    }
}