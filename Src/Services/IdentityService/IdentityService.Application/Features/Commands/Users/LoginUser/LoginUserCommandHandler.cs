using IdentityService.Application.Dtos;
using IdentityService.Application.Helper;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Application.Interfaces.Service;
using MediatR;

namespace IdentityService.Application.Features.Commands.Users.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AccessToken>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenHelper _tokenHelper;

    public LoginUserCommandHandler(IUserRepository userRepository, ITokenHelper tokenHelper)
    {
        _userRepository = userRepository;
        _tokenHelper = tokenHelper;
    }

    public async Task<AccessToken> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _userRepository.GetByEmailAsync(request.Email);

        if (currentUser == null)
            throw new InvalidOperationException($"{request.Email} User Not Found");

        if (!HashingHelper.VerifyPasswordHash(request.Password, currentUser.PasswordHash, currentUser.PasswordSalt))
            throw new InvalidOperationException("Wrong password");

        return _tokenHelper.CreateToken(currentUser);
    }
}