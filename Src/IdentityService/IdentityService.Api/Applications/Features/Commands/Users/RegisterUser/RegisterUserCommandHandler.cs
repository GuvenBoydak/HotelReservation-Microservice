using Bank.Application.Helper;
using Bank.Application.Interfaces.Repositories;
using Bank.Application.Interfaces.UnitOfWork;
using Bank.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Features.Commands.Users.RegisterUser;

public class RegisterUserCommandHandler : AsyncRequestHandler<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
    }

    protected override async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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
            user.Phone = request.Phone;
            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.RoleId = (await _roleRepository.Where(x => x.Name == "Admin").FirstOrDefaultAsync()).Id;
        }
        else
        {
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Phone = request.Phone;
            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.RoleId = (await _roleRepository.Where(x => x.Name == "User").FirstOrDefaultAsync()).Id;
        }

        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}