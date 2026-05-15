namespace DuelRecords.Blazor.Models;

public class YgoCardDetailsDto
{
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

    public string? HumanReadableCardType { get; set; }

    public string? ImageUrl { get; set; }

    public string? ImageUrlSmall { get; set; }

    public string? ImageUrlCropped { get; set; }
}