namespace MauiAdvices.Core.Entities;

public class Advice
{
    public Advice()
    {
        CreatedAt = DateTime.Now;
    }

    public int Id { get; set; }
    public string Text { get; set; }
    public string? TranslatedText { get; set; }
    public string? Author { get; set; }
    public string? Code { get; set; }
    public DateTime CreatedAt { get; set; }
}