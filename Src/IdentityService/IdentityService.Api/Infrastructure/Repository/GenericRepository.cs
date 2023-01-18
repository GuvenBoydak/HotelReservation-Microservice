using System.Linq.Expressions;
using IdentityService.Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.EntityFramework;
using Shared.Models;

namespace IdentityService.Api.Infrastructure.Repository;

public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly IdentityDbContext _db;

    public GenericRepository(IdentityDbContext db)
    {
        _db = db;
    }

    public DbSet<T> Table => _db.Set<T>();
    
    
    public async Task<T> GetById(Guid id)
    {
        return await Table.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> filter)
    {
        return Table.Where(filter);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await Table.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await Table.FindAsync(id);
        if (entity == null)
            throw new InvalidOperationException($"{nameof(entity)} Not found");
        
        entity.IsDeleted = true;
        Update(entity);
    }

    public void Update(T entity)
    {
        var currentEntity = Table.AsNoTracking().FirstOrDefault(x => x.Id == entity.Id);
        if (currentEntity == null)
            throw new InvalidOperationException($"{nameof(currentEntity)} Not found");

        _db.Entry(currentEntity).CurrentValues.SetValues(entity);

        foreach (var property in _db.Entry(entity).Properties)
        {
            if (property.CurrentValue != null && property.CurrentValue is not Guid)
                _db.Entry(entity).Property(property.Metadata.Name).IsModified = true;
        }
    }
}