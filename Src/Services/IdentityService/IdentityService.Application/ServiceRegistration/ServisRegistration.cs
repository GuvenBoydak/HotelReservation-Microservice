﻿using AutoMapper;
using FluentValidation.AspNetCore;
using IdentityService.Application.Dtos;
using IdentityService.Application.Mapping;
using IdentityService.Application.Validations;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Filters;
using System.Reflection;
using System.Text;

namespace IdentityService.Api.ServiceRegistration;

public static class ServisRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection service, IConfiguration configuration)
    {
        //MediatR
        service.AddMediatR(typeof(ServisRegistration));

        //mapper
        var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MapProfile()); });
        service.AddSingleton(mapperConfig.CreateMapper());

        //jwt
        TokenOption tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOption>();

        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
                };
            });
        
        // FluentValidation
        service.AddControllers(option => option.Filters.Add<ValidatorFilterAttribute>()).AddFluentValidation(x => 
            x.RegisterValidatorsFromAssemblyContaining(typeof(LoginUserCommandValidator)));

        return service;
    }
}