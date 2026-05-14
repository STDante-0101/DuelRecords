using System.Text.Json.Serialization;

namespace DuelRecords.Api.DTOs.Ygo;

public class YgoCardPriceDto
{
    [JsonPropertyName("cardmarket_price")]
    public decimal? CardMarketPrice { get; set; }

    [JsonPropertyName("tcgplayer_price")]
    public decimal? TcgPlayerPrice { get; set; }

    [JsonPropertyName("ebay_price")]
    public decimal? EbayPrice { get; set; }

    [JsonPropertyName("amazon_price")]
    public decimal? AmazonPrice { get; set; }

    [JsonPropertyName("coolstuffinc_price")]
    public decimal? CoolStuffIncPrice { get; set; }
}