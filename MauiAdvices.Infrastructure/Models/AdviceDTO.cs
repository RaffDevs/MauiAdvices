using System.Text.Json.Serialization;

namespace MauiAdvices.Infrastructure.Models;

public record AdviceDataDTO
{
    [JsonPropertyName("slip_id")] public int SlipId { get; init; }

    [JsonPropertyName("advice")] public string Advice { get; init; }
}

public record AdviceResponseDTO
{
    [JsonPropertyName("slip")] public AdviceDataDTO Data { get; init; }
}