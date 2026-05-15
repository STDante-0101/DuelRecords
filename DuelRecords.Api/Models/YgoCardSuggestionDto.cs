namespace DuelRecords.Blazor.Models;

public class YgoCardSuggestionDto
{
    public int YgoId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Type { get; set; }

    public string? Attribute { get; set; }

    public int? Level { get; set; }

    public int? Attack { get; set; }

    public int? Defense { get; set; }

    public string? ImageUrlSmall { get; set; }
}