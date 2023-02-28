using AutoMapper;
using IdentityService.Application.Features.Queries.GetByIdUser;
using IdentityService.Application.Helper;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.EntityFramework;

namespace IdentityService.Application.Features.Commands.Users.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork,
        IRoleRepository roleRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userEmail = await _userRepository.GetByEmailAsync(request.Email);

        if (userEmail != null)
            throw new InvalidOperationException($"{request.Email} User already exist");

        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
        var user = new User();

        if (request.Email == "admin@admin")
        {
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.RoleId = (await _roleRepository.Where(x => x.Name == "Admin").FirstOrDefaultAsync()).Id;
        }
        else
        {
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.RoleId = (await _roleRepository.Where(x => x.Name == "User").FirstOrDefaultAsync()).Id;
        }

        var result = await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<User, UserDto>(result);
    }
}