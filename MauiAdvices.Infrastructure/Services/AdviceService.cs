using System.Net.Http.Json;
using System.Text.Json;
using MauiAdvices.Infrastructure.Models;

namespace MauiAdvices.Infrastructure.Services;

public class AdviceService
{
    private readonly HttpClient _httpClient;

    public AdviceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AdviceResponseDTO?> GetRandomAdvice()
    {
        var uriBuilder = new UriBuilder
        {
            Scheme = "https",
            Host = "api.adviceslip.com",
            Path = "Advice"
        };

        var uri = uriBuilder.Uri;
        using var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        try
        {
            var result = await response.Content.ReadFromJsonAsync<AdviceResponseDTO>();
            return result;
        }
        catch (Exception ex)
        {
            throw new JsonException(ex.Message);
        }
        
    }
}