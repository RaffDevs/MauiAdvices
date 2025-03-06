namespace MauiAdvices.Interactors.Models;

public record AdviceDTO
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string? TranslatedText { get; set; }
    public string? Author { get; set; }
};