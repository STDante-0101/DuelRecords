using System.Net.Http.Json;
using DuelRecords.Blazor.Models;

namespace DuelRecords.Blazor.Services;

public class DeckPrefsApiService : IDeckPrefsApiService
{
    private readonly HttpClient _http;
    private readonly string _base;

    public DeckPrefsApiService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _base = config["ApiSettings:BaseUrl"] ?? string.Empty;
    }

    public async Task<DeckCoverPrefs> GetPrefsAsync(int deckId)
    {
        var response = await _http.GetAsync($"{_base}/api/deck-prefs/{deckId}");
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"HTTP {(int)response.StatusCode}: {body}");
        }
        var dto = await response.Content.ReadFromJsonAsync<DeckPrefsDto>();
        if (dto == null) return new();
        return new DeckCoverPrefs
        {
            BannerIndex = dto.BannerIndex,
            FeaturedCardId = dto.FeaturedCardId,
            FanCardIds = dto.FanCardIds,
            Tags = dto.Tags,
            CustomBannerImages = dto.CustomBannerImages,
            IsFavorite = dto.IsFavorite,
            GlowAttribute = dto.GlowAttribute
        };
    }

    public async Task SavePrefsAsync(int deckId, DeckCoverPrefs prefs)
    {
        var dto = new DeckPrefsDto
        {
            BannerIndex = prefs.BannerIndex,
            FeaturedCardId = prefs.FeaturedCardId,
            FanCardIds = prefs.FanCardIds,
            Tags = prefs.Tags,
            CustomBannerImages = prefs.CustomBannerImages,
            IsFavorite = prefs.IsFavorite,
            GlowAttribute = prefs.GlowAttribute
        };
        var response = await _http.PutAsJsonAsync($"{_base}/api/deck-prefs/{deckId}", dto);
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"HTTP {(int)response.StatusCode}: {body}");
        }
    }

    private class DeckPrefsDto
    {
        public int? BannerIndex { get; set; }
        public string? FeaturedCardId { get; set; }
        public List<string> FanCardIds { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public List<string> CustomBannerImages { get; set; } = new();
        public bool IsFavorite { get; set; }
        public string? GlowAttribute { get; set; }
    }
}
