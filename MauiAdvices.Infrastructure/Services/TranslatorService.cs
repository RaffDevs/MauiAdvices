using System.Net.Http.Json;
using MauiAdvices.Infrastructure.Models;
using Microsoft.Extensions.Configuration;

namespace MauiAdvices.Infrastructure.Services;

public class TranslatorService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public TranslatorService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<TranslatorResponseDTO> Translate(string sentence)
    {
        var uriBuilder = new UriBuilder
        {
            Scheme = "https",
            Host = _configuration.GetSection("TRANSLATOR_API")["HOST"],
            Path = "/translate?api-version=3.0&from=en&to=pt-br"
        };

        _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key",
            _configuration.GetSection("TRANSLATOR_API")["KEY"]);

        _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Region",
            _configuration.GetSection("TRANSLATOR_API")["REGION"]);

        var uri = uriBuilder.Uri;
        object[] json = new object[] { new { Text = sentence } };
        using var response = await _httpClient.PostAsJsonAsync(uri, json);
        response.EnsureSuccessStatusCode();
        try
        {
            var result = await response.Content.ReadFromJsonAsync<TranslatorResponseDTO>();
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}