namespace DuelRecords.Api.Models;

public class DeckPrefs
{
    public int Id { get; set; }
    public int DeckId { get; set; }
    public Deck Deck { get; set; } = default!;
    public int? BannerIndex { get; set; }
    public string? FeaturedCardId { get; set; }
    public string? FanCardIds { get; set; }          // JSON: string[]
    public string? Tags { get; set; }                 // JSON: string[]
    public string? CustomBannerImages { get; set; }   // JSON: string[]
    public bool IsFavorite { get; set; }
    public string? GlowAttribute { get; set; }
    public DateTime DataAtualizacao { get; set; }
}
