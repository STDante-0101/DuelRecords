namespace DuelRecords.Api.Models.Ygo;

public class YgoCardImage
{
    public int Id { get; set; }

    public int YgoCardId { get; set; }

    public YgoCard YgoCard { get; set; } = null!;

    public int ImageYgoId { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string? ImageUrlSmall { get; set; }

    public string? ImageUrlCropped { get; set; }

    public string? LocalImagePath { get; set; }

    public string? LocalSmallImagePath { get; set; }

    public string? LocalCroppedImagePath { get; set; }

    public bool Downloaded { get; set; }
}