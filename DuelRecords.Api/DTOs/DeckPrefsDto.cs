namespace DuelRecords.Api.DTOs;

public class DeckPrefsDto
{
    public int? BannerIndex { get; set; }
    public string? FeaturedCardId { get; set; }
    public List<string> FanCardIds { get; set; } = new();
    public List<string> Tags { get; set; } = new();
    public List<string> CustomBannerImages { get; set; } = new();
    public bool IsFavorite { get; set; }
    public string? GlowAttribute { get; set; }
}
