using System.Text.Json;
using DuelRecords.Api.Data;
using DuelRecords.Api.DTOs.Ygo;
using DuelRecords.Api.Models.Ygo;
using Microsoft.EntityFrameworkCore;

namespace DuelRecords.Api.Services.YgoServices;

public class YgoImportService
{
    private readonly AppDbContext _context;

    public YgoImportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> ImportFromJsonFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Arquivo JSON da YGO não encontrado.", filePath);
        }

        var json = await File.ReadAllTextAsync(filePath);

        var response = JsonSerializer.Deserialize<YgoApiResponseDto>(json);

        if (response?.Data == null || response.Data.Count == 0)
        {
            return 0;
        }

        var importedCount = 0;

        foreach (var cardDto in response.Data)
        {
            var existingCard = await _context.YgoCards
                .Include(c => c.Images)
                .Include(c => c.Sets)
                .Include(c => c.Prices)
                .FirstOrDefaultAsync(c => c.YgoId == cardDto.Id);

            if (existingCard == null)
            {
                var card = MapToEntity(cardDto);

                _context.YgoCards.Add(card);

                importedCount++;
            }
            else
            {
                UpdateEntity(existingCard, cardDto);

                importedCount++;
            }
        }

        await _context.SaveChangesAsync();

        return importedCount;
    }

    private static YgoCard MapToEntity(YgoCardDto dto)
    {
        var card = new YgoCard
        {
            YgoId = dto.Id,
            Name = dto.Name,
            Type = dto.Type,
            FrameType = dto.FrameType,
            Description = dto.Description,
            Race = dto.Race,
            Attribute = dto.Attribute,
            Archetype = dto.Archetype,
            Attack = dto.Attack,
            Defense = dto.Defense,
            Level = dto.Level,
            Scale = dto.Scale,
            LinkValue = dto.LinkValue,
            LinkMarkers = dto.LinkMarkers == null ? null : string.Join(",", dto.LinkMarkers),
            HumanReadableCardType = dto.HumanReadableCardType,
            ImportedAt = DateTime.UtcNow,
            LastSyncedAt = DateTime.UtcNow
        };

        if (dto.CardImages != null)
        {
            foreach (var imageDto in dto.CardImages)
            {
                card.Images.Add(new YgoCardImage
                {
                    ImageYgoId = imageDto.Id,
                    ImageUrl = imageDto.ImageUrl,
                    ImageUrlSmall = imageDto.ImageUrlSmall,
                    ImageUrlCropped = imageDto.ImageUrlCropped,
                    Downloaded = false
                });
            }
        }

        if (dto.CardSets != null)
        {
            foreach (var setDto in dto.CardSets)
            {
                card.Sets.Add(new YgoCardSet
                {
                    SetName = setDto.SetName,
                    SetCode = setDto.SetCode,
                    SetRarity = setDto.SetRarity,
                    SetRarityCode = setDto.SetRarityCode,
                    SetPrice = setDto.SetPrice
                });
            }
        }

        var priceDto = dto.CardPrices?.FirstOrDefault();

        if (priceDto != null)
        {
            card.Prices = new YgoCardPrice
            {
                CardMarketPrice = priceDto.CardMarketPrice,
                TcgPlayerPrice = priceDto.TcgPlayerPrice,
                EbayPrice = priceDto.EbayPrice,
                AmazonPrice = priceDto.AmazonPrice,
                CoolStuffIncPrice = priceDto.CoolStuffIncPrice
            };
        }

        return card;
    }

    private static void UpdateEntity(YgoCard existingCard, YgoCardDto dto)
    {
        existingCard.Name = dto.Name;
        existingCard.Type = dto.Type;
        existingCard.FrameType = dto.FrameType;
        existingCard.Description = dto.Description;
        existingCard.Race = dto.Race;
        existingCard.Attribute = dto.Attribute;
        existingCard.Archetype = dto.Archetype;
        existingCard.Attack = dto.Attack;
        existingCard.Defense = dto.Defense;
        existingCard.Level = dto.Level;
        existingCard.Scale = dto.Scale;
        existingCard.LinkValue = dto.LinkValue;
        existingCard.LinkMarkers = dto.LinkMarkers == null ? null : string.Join(",", dto.LinkMarkers);
        existingCard.HumanReadableCardType = dto.HumanReadableCardType;
        existingCard.LastSyncedAt = DateTime.UtcNow;
    }
}