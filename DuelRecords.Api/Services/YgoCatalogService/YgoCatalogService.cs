using DuelRecords.Api.Data.Contexts;
using DuelRecords.Api.DTOs.Ygo;
using Microsoft.EntityFrameworkCore;

namespace DuelRecords.Api.Services.YgoCatalogService;

public class YgoCatalogService
{
    private readonly AlexandriaDbContext _context;

    public YgoCatalogService(AlexandriaDbContext context)
    {
        _context = context;
    }

    public async Task<List<YgoCardSuggestionDto>> SearchByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Trim().Length < 2)
        {
            return new List<YgoCardSuggestionDto>();
        }

        var terms = name
            .Trim()
            .ToLower()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return await _context.YgoCards
            .AsNoTracking()
            .Where(card =>
                terms.All(term =>
                    card.Name.ToLower().Contains(term)))
            .OrderBy(card => card.Name)
            .Take(10)
            .Select(card => new YgoCardSuggestionDto
            {
                YgoId = card.YgoId,
                Name = card.Name,
                Type = card.Type,
                Attribute = card.Attribute,
                Level = card.Level,
                Attack = card.Attack,
                Defense = card.Defense,
                ImageUrlSmall = card.Images
                    .OrderBy(img => img.Id)
                    .Select(img => img.ImageUrlSmall)
                    .FirstOrDefault()
            })
            .ToListAsync();
    }

    public async Task<List<YgoSetSuggestionDto>> SearchBySetCodeAsync(string code)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Trim().Length < 2)
        {
            return new List<YgoSetSuggestionDto>();
        }

        var search = code.Trim().ToLower();

        return await _context.YgoCardSets
            .AsNoTracking()
            .Where(set =>
                set.SetCode.ToLower().Contains(search))
            .OrderBy(set => set.SetCode)
            .Take(10)
            .Select(set => new YgoSetSuggestionDto
            {
                YgoId = set.YgoCard.YgoId,
                Name = set.YgoCard.Name,
                SetCode = set.SetCode,
                SetName = set.SetName,
                SetRarity = set.SetRarity,

                ImageUrlSmall = set.YgoCard.Images
                    .OrderBy(img => img.Id)
                    .Select(img => img.ImageUrlSmall)
                    .FirstOrDefault()
            })
            .ToListAsync();
    }

    public async Task<YgoCardDetailsDto?> GetByYgoIdAsync(int ygoId)
    {
        return await _context.YgoCards
            .AsNoTracking()
            .Where(card => card.YgoId == ygoId)
            .Select(card => new YgoCardDetailsDto
            {
                YgoId = card.YgoId,
                Name = card.Name,
                Type = card.Type,
                FrameType = card.FrameType,
                Description = card.Description,
                Race = card.Race,
                Attribute = card.Attribute,
                Archetype = card.Archetype,
                Attack = card.Attack,
                Defense = card.Defense,
                Level = card.Level,
                HumanReadableCardType = card.HumanReadableCardType,

                ImageUrl = card.Images
                    .OrderBy(img => img.Id)
                    .Select(img => img.ImageUrl)
                    .FirstOrDefault(),

                ImageUrlSmall = card.Images
                    .OrderBy(img => img.Id)
                    .Select(img => img.ImageUrlSmall)
                    .FirstOrDefault(),

                ImageUrlCropped = card.Images
                    .OrderBy(img => img.Id)
                    .Select(img => img.ImageUrlCropped)
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();
    }
}