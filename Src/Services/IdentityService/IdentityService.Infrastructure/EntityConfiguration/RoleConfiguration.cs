using IdentityService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Infrastructure.EntityConfiguration;

public class RoleConfiguration:IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(40).IsRequired();
        builder.HasData(
            new Role() { Id = Guid.NewGuid(), CreatedDate = DateTime.UtcNow, Name = "Admin", IsDeleted = false },
            new Role() { Id = Guid.NewGuid(), CreatedDate = DateTime.UtcNow, Name = "User", IsDeleted = false });
    }
}