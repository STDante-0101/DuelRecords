using System.Net.Http.Json;
using DuelRecords.Blazor.Models;


namespace DuelRecords.Blazor.Services;

public class CardApiService : ICardApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public CardApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;

        _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? string.Empty;
    }

    public async Task<List<CardViewModel>> GetCardsAsync()
    {
        var cards = await _httpClient.GetFromJsonAsync<List<CardViewModel>>($"{_apiBaseUrl}/api/cards");

        return cards ?? new List<CardViewModel>();
    }

    public async Task<CardViewModel?> GetCardByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<CardViewModel>($"{_apiBaseUrl}/api/cards/{id}");
    }

    public async Task<bool> CreateCardAsync(CreateCardViewModel card)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/cards", card);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCardAsync(int id, CreateCardViewModel card)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/api/cards/{id}", card);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCardAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/cards/{id}");

        return response.IsSuccessStatusCode;
    }

    public string GetExportExcelUrl()
    {
        return $"{_apiBaseUrl}/api/cards/exportar-excel";
    }
}
