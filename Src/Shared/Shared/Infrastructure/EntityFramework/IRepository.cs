using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Shared.Infrastructure.EntityFramework;

public interface IRepository<T> where  T :BaseEntity
{
    DbSet<T> Table { get; }

    Task<T> GetById(Guid id);
    
    IQueryable<T> Where(Expression<Func<T,bool>> filter);

    Task<List<T>> GetAllAsync();

    Task AddAsync(T entity);

    Task DeleteAsync(Guid id);

    void Update(T entity);
}