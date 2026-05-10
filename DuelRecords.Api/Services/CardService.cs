using DuelRecords.Api.Data;
using DuelRecords.Api.DTOs;
using DuelRecords.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DuelRecords.Api.Services;

public class CardService : ICardService
{
    private readonly AppDbContext _context;

    public CardService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Card>> GetAllAsync()
    {
        return await _context.Cards
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<Card?> GetByIdAsync(int id)
    {
        return await _context.Cards.FindAsync(id);
    }

    public async Task<Card> CreateAsync(CreateCardDto dto)
    {
        var card = new Card
        {
            Nome = dto.Nome,
            Tipo = dto.Tipo,
            Atributo = dto.Atributo,
            Nivel = dto.Nivel,
            Ataque = dto.Ataque,
            Defesa = dto.Defesa,
            TipoDeck = dto.TipoDeck,
            Raridade = dto.Raridade,
            Quantidade = dto.Quantidade,
            Colecao = dto.Colecao,
            Descricao = dto.Descricao,
            ImagemUrl = dto.ImagemUrl,
            DataCadastro = DateTime.Now
        };

        _context.Cards.Add(card);
        await _context.SaveChangesAsync();

        return card;
    }

    public async Task<bool> UpdateAsync(int id, UpdateCardDto dto)
    {
        var card = await _context.Cards.FindAsync(id);

        if (card == null)
        {
            return false;
        }

        card.Nome = dto.Nome;
        card.Tipo = dto.Tipo;
        card.Atributo = dto.Atributo;
        card.Nivel = dto.Nivel;
        card.Ataque = dto.Ataque;
        card.Defesa = dto.Defesa;
        card.TipoDeck = dto.TipoDeck;
        card.Raridade = dto.Raridade;
        card.Quantidade = dto.Quantidade;
        card.Colecao = dto.Colecao;
        card.Descricao = dto.Descricao;
        card.ImagemUrl = dto.ImagemUrl;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var card = await _context.Cards
            .FirstOrDefaultAsync(c => c.Id == id);

        if (card == null)
        {
            return false;
        }

        var deckCards = await _context.DeckCards
            .Where(dc => dc.CardId == id)
            .ToListAsync();

        if (deckCards.Count > 0)
        {
            _context.DeckCards.RemoveRange(deckCards);
        }

        _context.Cards.Remove(card);

        await _context.SaveChangesAsync();

        return true;
    }
}