using LiteDB;
using LiteDB.Async;
using MauiAdvices.Core.Entities;
using MauiAdvices.Core.Repositories;
using MauiAdvices.Infrastructure.Persistence.Database;

namespace MauiAdvices.Infrastructure.Persistence.Repositories;

public class AdviceRepository : IAdviceRepository
{
    private readonly AdvicesDatabase _databaseFactory;
    private readonly LiteDatabaseAsync _database;
    private const string CollectionName = "advices";
    public AdviceRepository(AdvicesDatabase factory)
    {
        _databaseFactory = factory;
        _database = _databaseFactory.GetDatabase();

    }

    public async Task<IEnumerable<Advice>> Get()
    {
        var collection = _database.GetCollection<Advice>(CollectionName);
        var advices = await collection.FindAllAsync();
        return advices.ToList();
    }

    public async Task Create(Advice advice)
    {
        var collection = _database.GetCollection<Advice>(CollectionName);
        await collection.InsertAsync(advice);
        await collection.EnsureIndexAsync(c => c.Text);
        await _database.CheckpointAsync();
    }

    public async Task Delete(int id)
    {
        var collection = _database.GetCollection<Advice>(CollectionName);
        await collection.DeleteAsync(id);
        await _database.CheckpointAsync();
    }
}