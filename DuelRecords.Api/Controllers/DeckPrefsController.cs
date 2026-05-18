using DuelRecords.Api.Data.Contexts;
using DuelRecords.Api.DTOs;
using DuelRecords.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DuelRecords.Api.Controllers;

[ApiController]
[Route("api/deck-prefs")]
public class DeckPrefsController : ControllerBase
{
    private readonly MundoZeroDbContext _db;

    public DeckPrefsController(MundoZeroDbContext db) => _db = db;

    [HttpGet("{deckId:int}")]
    public async Task<ActionResult<DeckPrefsDto>> Get(int deckId)
    {
        var prefs = await _db.DeckPrefs.FirstOrDefaultAsync(p => p.DeckId == deckId);
        if (prefs == null) return Ok(new DeckPrefsDto());
        return Ok(ToDto(prefs));
    }

    [HttpPut("{deckId:int}")]
    public async Task<IActionResult> Upsert(int deckId, DeckPrefsDto dto)
    {
        var prefs = await _db.DeckPrefs.FirstOrDefaultAsync(p => p.DeckId == deckId);

        if (prefs == null)
        {
            prefs = new DeckPrefs { DeckId = deckId };
            _db.DeckPrefs.Add(prefs);
        }

        prefs.BannerIndex = dto.BannerIndex;
        prefs.FeaturedCardId = dto.FeaturedCardId;
        prefs.FanCardIds = dto.FanCardIds.Count > 0 ? JsonSerializer.Serialize(dto.FanCardIds) : null;
        prefs.Tags = dto.Tags.Count > 0 ? JsonSerializer.Serialize(dto.Tags) : null;
        prefs.CustomBannerImages = dto.CustomBannerImages.Count > 0 ? JsonSerializer.Serialize(dto.CustomBannerImages) : null;
        prefs.IsFavorite = dto.IsFavorite;
        prefs.GlowAttribute = dto.GlowAttribute;
        prefs.DataAtualizacao = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    private static DeckPrefsDto ToDto(DeckPrefs p) => new()
    {
        BannerIndex = p.BannerIndex,
        FeaturedCardId = p.FeaturedCardId,
        FanCardIds = Deserialize(p.FanCardIds),
        Tags = Deserialize(p.Tags),
        CustomBannerImages = Deserialize(p.CustomBannerImages),
        IsFavorite = p.IsFavorite,
        GlowAttribute = p.GlowAttribute
    };

    private static List<string> Deserialize(string? json)
    {
        if (string.IsNullOrEmpty(json)) return new();
        try { return JsonSerializer.Deserialize<List<string>>(json) ?? new(); }
        catch { return new(); }
    }
}
