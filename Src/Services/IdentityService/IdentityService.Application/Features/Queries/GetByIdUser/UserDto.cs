﻿namespace IdentityService.Application.Features.Queries.GetByIdUser;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Guid RoleId { get; set; }
}