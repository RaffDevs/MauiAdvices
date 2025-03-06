using LiteDB;
using LiteDB.Async;

namespace MauiAdvices.Infrastructure.Persistence.Database;

public class AdvicesDatabase
{
    private string ConnectionString { get; set; }
    private readonly LiteDatabaseAsync _database;

    public AdvicesDatabase(string connectionString)
    {
        ConnectionString = connectionString;
        _database = new LiteDatabaseAsync(connectionString);
    }

    public LiteDatabaseAsync GetDatabase() => _database;
}