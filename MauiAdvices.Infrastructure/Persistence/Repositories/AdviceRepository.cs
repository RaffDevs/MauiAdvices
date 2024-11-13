using MauiAdvices.Core.Entities;
using MauiAdvices.Core.Repositories;
using MauiAdvices.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MauiAdvices.Infrastructure.Persistence.Repositories;

public class AdviceRepository : IAdviceRepository
{
    private readonly MauiAdvicesDatabaseContext _context;

    public AdviceRepository(MauiAdvicesDatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Advice>> Get()
    {
        return await _context.Advices.ToListAsync();
    }

    public async Task<Advice> Create(Advice advice)
    {
        var insertedItem = _context.Advices.Add(advice);
        await _context.SaveChangesAsync();

        return insertedItem.Entity;
    }

    public async Task Delete(Advice advice)
    {
        _context.Advices.Remove(advice);
        await _context.SaveChangesAsync();
    }
}