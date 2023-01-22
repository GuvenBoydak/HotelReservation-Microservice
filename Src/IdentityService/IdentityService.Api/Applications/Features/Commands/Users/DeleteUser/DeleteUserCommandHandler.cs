using Bank.Application.Interfaces.Repositories;
using Bank.Application.Interfaces.UnitOfWork;
using MediatR;

namespace Bank.Application.Features.Commands.Users.DeleteUser;

public class DeleteUserCommandHandler : AsyncRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    protected override async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id);

        if (user == null)
            throw new InvalidOperationException("User not found");

        await _userRepository.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}