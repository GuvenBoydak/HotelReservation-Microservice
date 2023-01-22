using Bank.Application.DTOs.Token;
using Bank.Application.Helper;
using Bank.Application.Interfaces.Repositories;
using Bank.Application.Interfaces.Services;
using MediatR;

namespace Bank.Application.Features.Commands.Users.LoginUser;

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