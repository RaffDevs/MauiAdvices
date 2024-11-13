using MauiAdvices.Core.Entities;

namespace MauiAdvices.Core.Repositories;

public interface IAdviceRepository
{
    Task<IEnumerable<Advice>> Get();
    Task<Advice> Create(Advice advice);
    Task Delete(Advice advice);
}