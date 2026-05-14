using System.Text.Json.Serialization;

namespace DuelRecords.Api.DTOs.Ygo;

public class YgoCardImageDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; } = string.Empty;

    [JsonPropertyName("image_url_small")]
    public string? ImageUrlSmall { get; set; }

    [JsonPropertyName("image_url_cropped")]
    public string? ImageUrlCropped { get; set; }
}