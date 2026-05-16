namespace DuelRecords.Api.Models.Ygo;

public class YgoCard
{
    public int Id { get; set; }

    public int YgoId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string? FrameType { get; set; }

    public string? Description { get; set; }

    public string? Race { get; set; }

    public string? Attribute { get; set; }

    public string? Archetype { get; set; }

    public int? Attack { get; set; }

    public int? Defense { get; set; }

    public int? Level { get; set; }

    public int? Scale { get; set; }

    public int? LinkValue { get; set; }

    public string? LinkMarkers { get; set; }

    public string? HumanReadableCardType { get; set; }

    public string? LocalImagePath { get; set; }

    public DateTime ImportedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastSyncedAt { get; set; } = DateTime.UtcNow;

    public List<YgoCardImage> Images { get; set; } = new();

    public List<YgoCardSet> Sets { get; set; } = new();

    public YgoCardPrice? Prices { get; set; }
}