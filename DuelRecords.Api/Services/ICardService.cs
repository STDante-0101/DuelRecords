using DuelRecords.Api.DTOs;
using DuelRecords.Api.Models;

namespace DuelRecords.Api.Services;

public interface ICardService
{
    Task<List<Card>> GetAllAsync();
    Task<Card?> GetByIdAsync(int id);
    Task<Card> CreateAsync(CreateCardDto dto);
    Task<bool> UpdateAsync(int id, UpdateCardDto dto);
    Task<bool> DeleteAsync(int id);
}
