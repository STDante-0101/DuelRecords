using System.Text.Json.Serialization;

namespace DuelRecords.Api.DTOs.Ygo;

public class YgoCardDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("frameType")]
    public string? FrameType { get; set; }

    [JsonPropertyName("desc")]
    public string? Description { get; set; }

    [JsonPropertyName("race")]
    public string? Race { get; set; }

    [JsonPropertyName("attribute")]
    public string? Attribute { get; set; }

    [JsonPropertyName("archetype")]
    public string? Archetype { get; set; }

    [JsonPropertyName("atk")]
    public int? Attack { get; set; }

    [JsonPropertyName("def")]
    public int? Defense { get; set; }

    [JsonPropertyName("level")]
    public int? Level { get; set; }

    [JsonPropertyName("scale")]
    public int? Scale { get; set; }

    [JsonPropertyName("linkval")]
    public int? LinkValue { get; set; }

    [JsonPropertyName("linkmarkers")]
    public List<string>? LinkMarkers { get; set; }

    [JsonPropertyName("humanReadableCardType")]
    public string? HumanReadableCardType { get; set; }

    [JsonPropertyName("card_images")]
    public List<YgoCardImageDto>? CardImages { get; set; }

    [JsonPropertyName("card_sets")]
    public List<YgoCardSetDto>? CardSets { get; set; }

    [JsonPropertyName("card_prices")]
    public List<YgoCardPriceDto>? CardPrices { get; set; }
}