using Shared.Models;

namespace IdentityService.Api.Models;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public Guid RoleId { get; set; }

    //Relational property
    public Role Role { get; set; }
}

