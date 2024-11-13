using MauiAdvices.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MauiAdvices.Infrastructure.Persistence.Context;

public class MauiAdvicesDatabaseContext : DbContext
{
    public DbSet<Advice> Advices { get; set; }

    public MauiAdvicesDatabaseContext(DbContextOptions<MauiAdvicesDatabaseContext> options) : base(options)
    {
        SQLitePCL.Batteries_V2.Init();
        this.Database.EnsureCreated();
    }

}