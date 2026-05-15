namespace DuelRecords.Api.Models.Ygo;

public class YgoCardPrice
{
    public int Id { get; set; }

    public int YgoCardId { get; set; }

    public YgoCard YgoCard { get; set; } = null!;

    public string? CardMarketPrice { get; set; }

    public string? TcgPlayerPrice { get; set; }

    public string? EbayPrice { get; set; }

    public string? AmazonPrice { get; set; }

    public string? CoolStuffIncPrice { get; set; }
}