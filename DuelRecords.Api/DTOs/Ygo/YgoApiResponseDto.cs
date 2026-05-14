using System.Text.Json.Serialization;

namespace DuelRecords.Api.DTOs.Ygo;

public class YgoApiResponseDto
{
    [JsonPropertyName("data")]
    public List<YgoCardDto> Data { get; set; } = new();
}