using System.Text.Json.Serialization;

namespace MauiAdvices.Infrastructure.Models;

public record TranslatorResponseDTO
{
    [JsonPropertyName("translations")]
    public List<TranslatedItemDTO> Translations { get; init; }
};

public record TranslatedItemDTO
{
    [JsonPropertyName("text")]
    public string Text { get; init; }
    
    [JsonPropertyName("to")]
    public string To { get; init; }
}


