using System.Net.Http.Json;
using DuelRecords.Blazor.Models;

namespace DuelRecords.Blazor.Services;

public class YgoCatalogApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public YgoCatalogApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? string.Empty;
    }

    public async Task<List<YgoCardSuggestionDto>> SearchAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Trim().Length < 2)
        {
            return new List<YgoCardSuggestionDto>();
        }

        var query = Uri.EscapeDataString(name.Trim());

        return await _httpClient.GetFromJsonAsync<List<YgoCardSuggestionDto>>(
            $"{_apiBaseUrl}/api/ygo/cards/buscar?nome={query}"
        ) ?? new List<YgoCardSuggestionDto>();
    }

    public async Task<YgoCardDetailsDto?> GetByYgoIdAsync(int ygoId)
    {
        return await _httpClient.GetFromJsonAsync<YgoCardDetailsDto>(
            $"{_apiBaseUrl}/api/ygo/cards/{ygoId}"
        );
    }
}