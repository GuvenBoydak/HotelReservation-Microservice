using AutoMapper;
using Bank.Application.Interfaces.Repositories;
using Bank.Domain.Models;
using MediatR;

namespace Bank.Application.Features.Queries.Users.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserListDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<UserListDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();

        return _mapper.Map<List<User>, List<UserListDto>>(users);
    }
}