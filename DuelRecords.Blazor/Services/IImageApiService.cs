namespace DuelRecords.Blazor.Services;

public interface IImageApiService
{
    Task<(string? Url, string? Error)> UploadDeckCoverAsync(Stream fileStream, string fileName);
}
