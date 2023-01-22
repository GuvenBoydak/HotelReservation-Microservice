using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure.EntityFramework.UnitOfWork;

public class UnitOfWork<TContext>where TContext:DbContext,IUnitOfWork
{

    private readonly TContext _db;

    public UnitOfWork(TContext db)
    {
        _db = db;
    }
}