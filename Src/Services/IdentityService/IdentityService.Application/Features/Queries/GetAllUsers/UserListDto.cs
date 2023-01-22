namespace IdentityService.Api.Applications.Features.Queries.GetAllUsers;

public class UserListDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Guid RoleId { get; set; }
}