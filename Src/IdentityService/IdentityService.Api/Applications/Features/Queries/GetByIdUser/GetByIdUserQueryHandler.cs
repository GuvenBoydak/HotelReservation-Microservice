using AutoMapper;
using Bank.Application.Interfaces.Repositories;
using Bank.Domain.Models;
using MediatR;

namespace Bank.Application.Features.Queries.Users.GetByIdUser;

public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery,UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetByIdUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id);

        if (user == null)
            throw new InvalidOperationException("User Not Found");

        return _mapper.Map<User, UserDto>(user);
    }
}