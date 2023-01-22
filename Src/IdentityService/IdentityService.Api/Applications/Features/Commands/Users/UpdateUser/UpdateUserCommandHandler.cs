using AutoMapper;
using Bank.Application.Interfaces.Repositories;
using Bank.Application.Interfaces.UnitOfWork;
using Bank.Domain.Models;
using MediatR;

namespace Bank.Application.Features.Commands.Users.UpdateUser;

public class UpdateUserCommandHandler : AsyncRequestHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    protected override async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<UpdateUserCommand, User>(request);

        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();
    }
}