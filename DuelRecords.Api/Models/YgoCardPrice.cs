namespace DuelRecords.Api.Models.Ygo;

public class YgoCardPrice
{
    public int Id { get; set; }

    public int YgoCardId { get; set; }

    public YgoCard YgoCard { get; set; } = null!;

    public decimal? CardMarketPrice { get; set; }

    public decimal? TcgPlayerPrice { get; set; }

    public decimal? EbayPrice { get; set; }

    public decimal? AmazonPrice { get; set; }

    public decimal? CoolStuffIncPrice { get; set; }
}