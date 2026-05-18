namespace DuelRecords.Blazor.Services;

public class ImageApiService : IImageApiService
{
    private readonly IWebHostEnvironment _env;

    public ImageApiService(IWebHostEnvironment env)
    {
        _env = env;
    }

    private static readonly string[] AllowedExtensions = { ".png", ".jpg", ".jpeg", ".webp" };

    public async Task<(string? Url, string? Error)> UploadDeckCoverAsync(Stream fileStream, string fileName)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(ext))
            return (null, "Formato inválido. Use PNG, JPG, JPEG ou WEBP.");

        try
        {
            var webRoot = _env.WebRootPath
                ?? Path.Combine(_env.ContentRootPath, "wwwroot");

            var folder = Path.Combine(webRoot, "uploads", "deck-covers");
            Directory.CreateDirectory(folder);

            var savedName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(folder, savedName);

            await using var output = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            await fileStream.CopyToAsync(output);

            return ($"/uploads/deck-covers/{savedName}", null);
        }
        catch (Exception ex)
        {
            return (null, $"Erro ao salvar imagem: {ex.Message}");
        }
    }
}
