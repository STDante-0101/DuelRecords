using DuelRecords.Api.Data.Contexts;
using DuelRecords.Api.DTOs.Decks;
using DuelRecords.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DuelRecords.Api.Services;

public class DeckService : IDeckService
{
    private readonly MundoZeroDbContext _context;

    public DeckService(MundoZeroDbContext context)
    {
        _context = context;
    }

    public async Task<List<DeckResponseDto>> GetAllAsync()
    {
        var decks = await _context.Decks
            .Include(d => d.Cards)
            .ThenInclude(dc => dc.Card)
            .OrderByDescending(d => d.DataAtualizacao)
            .ToListAsync();

        return decks.Select(MapToResponse).ToList();
    }

    public async Task<DeckResponseDto?> GetByIdAsync(int id)
    {
        var deck = await _context.Decks
            .Include(d => d.Cards)
            .ThenInclude(dc => dc.Card)
            .FirstOrDefaultAsync(d => d.Id == id);

        return deck == null ? null : MapToResponse(deck);
    }

    public async Task<DeckResponseDto> CreateAsync(CreateDeckDto dto)
    {
        var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

        var deck = new Deck
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            DataCriacao = now,
            DataAtualizacao = now,
            Cards = dto.Cards.Select(c => new DeckCard
            {
                CardId = c.CardId,
                Quantidade = c.Quantidade,
                Secao = c.Secao,
                Ordem = c.Ordem
            }).ToList()
        };

        _context.Decks.Add(deck);

        await _context.SaveChangesAsync();

        return (await GetByIdAsync(deck.Id))!;
    }

    public async Task<bool> UpdateAsync(int id, UpdateDeckDto dto)
    {
        var deck = await _context.Decks
            .Include(d => d.Cards)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (deck == null)
        {
            return false;
        }

        deck.Nome = dto.Nome;
        deck.Descricao = dto.Descricao;
        deck.DataAtualizacao = DateTime.UtcNow;

        _context.DeckCards.RemoveRange(deck.Cards);

        deck.Cards = dto.Cards.Select(c => new DeckCard
        {
            DeckId = deck.Id,
            CardId = c.CardId,
            Quantidade = c.Quantidade,
            Secao = c.Secao,
            Ordem = c.Ordem
        }).ToList();

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var deck = await _context.Decks.FindAsync(id);

        if (deck == null)
        {
            return false;
        }

        _context.Decks.Remove(deck);
        await _context.SaveChangesAsync();

        return true;
    }

    private static DeckResponseDto MapToResponse(Deck deck)
    {
        return new DeckResponseDto
        {
            Id = deck.Id,
            Nome = deck.Nome,
            Descricao = deck.Descricao,
            DataCriacao = deck.DataCriacao,
            DataAtualizacao = deck.DataAtualizacao,
            Cards = deck.Cards
                .OrderBy(c => c.Secao)
                .ThenBy(c => c.Ordem)
                .Select(c => new DeckCardResponseDto
                {
                    Id = c.Id,
                    CardId = c.CardId,
                    NomeCard = c.Card.Nome,
                    Tipo = c.Card.Tipo,
                    TipoDeck = c.Card.TipoDeck,
                    ImagemUrl = c.Card.ImagemUrl,
                    Quantidade = c.Quantidade,
                    Secao = c.Secao.ToString(),
                    Ordem = c.Ordem
                })
                .ToList()
        };
    }
}