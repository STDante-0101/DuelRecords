using DuelRecords.Blazor.Models;

namespace DuelRecords.Blazor.Services;

public interface IDeckApiService
{
    Task<List<DeckApiModel>> GetDecksAsync();
    Task<DeckApiModel?> GetDeckByIdAsync(int id);
    Task<DeckApiModel?> CreateDeckAsync(CreateDeckApiModel deck);
    Task<bool> UpdateDeckAsync(int id, CreateDeckApiModel deck);
    Task<bool> DeleteDeckAsync(int id);
}