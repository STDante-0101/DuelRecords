using Microsoft.AspNetCore.Mvc;

namespace DuelRecords.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;

    public ImagesController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpPost("deck-covers")]
    [RequestSizeLimit(31_457_280)]
    public async Task<IActionResult> UploadDeckCover(IFormFile arquivo)
    {
        if (arquivo == null || arquivo.Length == 0)
            return BadRequest("Arquivo inválido.");

        var ext = Path.GetExtension(arquivo.FileName).ToLowerInvariant();
        var allowed = new[] { ".png", ".jpg", ".jpeg", ".webp" };
        if (!allowed.Contains(ext))
            return BadRequest("Formato inválido. Use PNG, JPG, JPEG ou WEBP.");

        var webRoot = _environment.WebRootPath
            ?? Path.Combine(_environment.ContentRootPath, "wwwroot");
        var folder = Path.Combine(webRoot, "uploads", "deck-covers");
        Directory.CreateDirectory(folder);

        var fileName = $"{Guid.NewGuid()}{ext}";
        var filePath = Path.Combine(folder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
            await arquivo.CopyToAsync(stream);

        return Ok(new { url = $"/uploads/deck-covers/{fileName}" });
    }
}
