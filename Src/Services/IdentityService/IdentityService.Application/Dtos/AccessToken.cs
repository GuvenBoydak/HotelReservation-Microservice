﻿namespace IdentityService.Application.Dtos;

public class AccessToken
{
    public string Token { get; set; }

    public DateTime Expiration { get; set; }
}