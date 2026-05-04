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
        return await _context.Cards.ToListAsync();
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
            TipoCard = dto.TipoCard,
            Atributo = dto.Atributo,
            Nivel = dto.Nivel,
            Ataque = dto.Ataque,
            Defesa = dto.Defesa,
            Descricao = dto.Descricao,
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
        card.TipoCard = dto.TipoCard;
        card.Atributo = dto.Atributo;
        card.Nivel = dto.Nivel;
        card.Ataque = dto.Ataque;
        card.Defesa = dto.Defesa;
        card.Descricao = dto.Descricao;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var card = await _context.Cards.FindAsync(id);

        if (card == null)
        {
            return false;
        }

        _context.Cards.Remove(card);
        await _context.SaveChangesAsync();

        return true;
    }
}
