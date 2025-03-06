using MauiAdvices.Infrastructure.Persistence.Context;

namespace MauiAdvices.Migrator;

public static class Program
{
    public static void Main(string[] args)
    {
        using (var dbContext = new MauiAdvicesDatabaseContext())
        {
            var advices = dbContext.Advices.ToList();
        }
    }
}