using System.Net.Http.Json;
using DuelRecords.Blazor.Models;

namespace DuelRecords.Blazor.Services;

public class DeckApiService : IDeckApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public DeckApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? string.Empty;
    }

    public async Task<List<DeckApiModel>> GetDecksAsync()
    {
        var decks = await _httpClient.GetFromJsonAsync<List<DeckApiModel>>($"{_apiBaseUrl}/api/decks");

        return decks ?? new List<DeckApiModel>();
    }

    public async Task<DeckApiModel?> GetDeckByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<DeckApiModel>($"{_apiBaseUrl}/api/decks/{id}");
    }

    public async Task<DeckApiModel?> CreateDeckAsync(CreateDeckApiModel deck)
    {
        Console.WriteLine("=== CREATE DECK ===");

        Console.WriteLine($"Nome: {deck.Nome}");

        foreach (var card in deck.Cards)
        {
            Console.WriteLine(
                $"CardId={card.CardId} | Qtd={card.Quantidade} | Secao={card.Secao}");
        }

        var response = await _httpClient.PostAsJsonAsync(
            $"{_apiBaseUrl}/api/decks",
            deck);

        Console.WriteLine($"Status Code: {response.StatusCode}");

        var content = await response.Content.ReadAsStringAsync();

        Console.WriteLine("Resposta API:");
        Console.WriteLine(content);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("ERRO AO CRIAR DECK");
            return null;
        }

        return await response.Content.ReadFromJsonAsync<DeckApiModel>();
    }

    public async Task<bool> UpdateDeckAsync(int id, CreateDeckApiModel deck)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/api/decks/{id}", deck);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteDeckAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/decks/{id}");

        return response.IsSuccessStatusCode;
    }
}