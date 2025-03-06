using MauiAdvices.Core.Entities;

namespace MauiAdvices.Core.Repositories;

public interface IAdviceRepository
{
    Task<IEnumerable<Advice>> Get();
    Task Create(Advice advice);
    Task Delete(int id);
}