namespace DuelRecords.Api.Models.Ygo;

public class YgoCardSet
{
    public int Id { get; set; }

    public int YgoCardId { get; set; }

    public YgoCard YgoCard { get; set; } = null!;

    public string SetName { get; set; } = string.Empty;

    public string SetCode { get; set; } = string.Empty;

    public string? SetRarity { get; set; }

    public string? SetRarityCode { get; set; }

    public decimal? SetPrice { get; set; }
}