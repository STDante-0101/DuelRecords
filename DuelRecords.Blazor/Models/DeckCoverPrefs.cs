namespace DuelRecords.Blazor.Models;

public class DeckCoverPrefs
{
    public int? BannerIndex { get; set; }
    public string? FeaturedCardId { get; set; }
    public List<string> FanCardIds { get; set; } = new();
    public List<string> Tags { get; set; } = new();
    public List<string> CustomBannerImages { get; set; } = new();
    public bool IsFavorite { get; set; } = false;
    public string? GlowAttribute { get; set; }
}
