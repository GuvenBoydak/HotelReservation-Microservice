using IdentityService.Infrastructure.Context;
using Shared.Infrastructure.EntityFramework;

namespace IdentityService.Infrastructure.Repository
{
    public class IdentityUnitOfWork:IUnitOfWork
    {
        private readonly IdentityDbContext _db;

        public IdentityUnitOfWork(IdentityDbContext db)
        {
            _db = db;
        }

        public async Task SaveChangesAsync()
        {
           await _db.SaveChangesAsync();
        }
    }
}
