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
        try
        {
            var dto = await _http.GetFromJsonAsync<DeckPrefsDto>($"{_base}/api/deck-prefs/{deckId}");
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
        catch { return new(); }
    }

    public async Task SavePrefsAsync(int deckId, DeckCoverPrefs prefs)
    {
        try
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
            await _http.PutAsJsonAsync($"{_base}/api/deck-prefs/{deckId}", dto);
        }
        catch { }
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
