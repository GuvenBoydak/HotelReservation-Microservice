namespace Shared.Infrastructure.EntityFramework;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}