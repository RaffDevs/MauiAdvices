namespace MauiAdvices.Core.Entities;

public class Advice
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string DescriptionTranslated { get; set; }
    public DateTime CreatedAt { get; set; }

    public Advice()
    {
        CreatedAt = DateTime.Now;
    }
}