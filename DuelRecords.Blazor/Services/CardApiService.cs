using System.Net.Http.Json;
using DuelRecords.Blazor.Models;

namespace DuelRecords.Blazor.Services;

public class CardApiService : ICardApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public CardApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? string.Empty;
    }

    public async Task<List<Card>> GetCardsAsync()
    {
        var apiCards = await _httpClient.GetFromJsonAsync<List<ApiCardDto>>($"{_apiBaseUrl}/api/cards");

        return apiCards?
            .Select(MapToCard)
            .ToList() ?? new List<Card>();
    }

    public async Task<Card?> GetCardByIdAsync(int id)
    {
        var apiCard = await _httpClient.GetFromJsonAsync<ApiCardDto>($"{_apiBaseUrl}/api/cards/{id}");

        return apiCard is null ? null : MapToCard(apiCard);
    }

    public async Task<bool> CreateCardAsync(CreateCardViewModel card)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/cards", card);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCardAsync(int id, Card card)
    {
        var dto = new CreateCardViewModel
        {
            Nome = card.Name,
            Tipo = card.Type,
            Atributo = card.Attribute,
            Nivel = card.Level,
            Ataque = card.Atk,
            Defesa = card.Def,
            TipoDeck = card.Kind,
            Raridade = card.Rarity,
            Quantidade = card.Qty,
            Colecao = card.Set,
            Descricao = card.Description,
            ImagemUrl = card.ImageUrl
        };

        var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/api/cards/{id}", dto);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCardAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/cards/{id}");

        return response.IsSuccessStatusCode;
    }

    public async Task<string?> UploadImagemAsync(Stream fileStream, string fileName)
    {
        using var formData = new MultipartFormDataContent();

        using var fileContent = new StreamContent(fileStream);

        var contentType = GetContentType(fileName);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

        formData.Add(fileContent, "arquivo", fileName);

        var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/cards/upload-imagem", formData);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var uploadResponse = await response.Content.ReadFromJsonAsync<UploadImagemResponse>();

        if (uploadResponse == null || string.IsNullOrWhiteSpace(uploadResponse.ImagemUrl))
        {
            return null;
        }

        return $"{_apiBaseUrl}{uploadResponse.ImagemUrl}";
    }

    public string GetExportExcelUrl()
    {
        return $"{_apiBaseUrl}/api/cards/exportar-excel";
    }

    private static string GetContentType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();

        return extension switch
        {
            ".png" => "image/png",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            _ => "application/octet-stream"
        };
    }

    private static string? CorrigirUrlImagem(string? imagemUrl)
    {
        if (string.IsNullOrWhiteSpace(imagemUrl))
        {
            return null;
        }

        if (imagemUrl.StartsWith("http://duelrecords-api:8080"))
        {
            return imagemUrl.Replace(
                "http://duelrecords-api:8080",
                "http://192.168.0.2:8080"
            );
        }

        return imagemUrl;
    }

    private static Card MapToCard(ApiCardDto dto)
    {
        return new Card
        {
            Id = dto.Id.ToString(),
            Name = dto.Nome,
            Type = dto.Tipo,
            Attribute = dto.Atributo,
            Level = dto.Nivel,
            Atk = dto.Ataque,
            Def = dto.Defesa,
            Kind = dto.TipoDeck,
            Rarity = dto.Raridade,
            Qty = dto.Quantidade,
            Set = dto.Colecao,
            Description = dto.Descricao,
            ImageUrl = CorrigirUrlImagem(dto.ImagemUrl)
        };
    }

    private class ApiCardDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string? Atributo { get; set; }
        public int? Nivel { get; set; }
        public int? Ataque { get; set; }
        public int? Defesa { get; set; }
        public string TipoDeck { get; set; } = "Normal";
        public string Raridade { get; set; } = "Comum";
        public int Quantidade { get; set; } = 1;
        public string Colecao { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string? ImagemUrl { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    private class UploadImagemResponse
    {
        public string? ImagemUrl { get; set; }
    }
}