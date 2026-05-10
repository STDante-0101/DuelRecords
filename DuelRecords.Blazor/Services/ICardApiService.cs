using DuelRecords.Blazor.Models;

namespace DuelRecords.Blazor.Services;

public interface ICardApiService
{
    Task<List<Card>> GetCardsAsync();
    Task<Card?> GetCardByIdAsync(int id);
    Task<bool> CreateCardAsync(CreateCardViewModel card);
    Task<bool> UpdateCardAsync(int id, Card card);
    Task<bool> DeleteCardAsync(int id);
    string GetExportExcelUrl();
    Task<string?> UploadImagemAsync(Stream fileStream, string fileName);
}