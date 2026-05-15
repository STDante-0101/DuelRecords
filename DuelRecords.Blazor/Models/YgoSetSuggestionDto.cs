namespace DuelRecords.Blazor.Models;

public class YgoSetSuggestionDto
{
    public int YgoId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string SetCode { get; set; } = string.Empty;

    public string? SetName { get; set; }

    public string? SetRarity { get; set; }

    public string? ImageUrlSmall { get; set; }
}