using System.Text.Json.Serialization;

namespace DuelRecords.Api.DTOs.Ygo;

public class YgoCardPriceDto
{
    [JsonPropertyName("cardmarket_price")]
    public string? CardMarketPrice { get; set; }

    [JsonPropertyName("tcgplayer_price")]
    public string? TcgPlayerPrice { get; set; }

    [JsonPropertyName("ebay_price")]
    public string? EbayPrice { get; set; }

    [JsonPropertyName("amazon_price")]
    public string? AmazonPrice { get; set; }

    [JsonPropertyName("coolstuffinc_price")]
    public string? CoolStuffIncPrice { get; set; }
}