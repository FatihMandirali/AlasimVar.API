using AlasimVar.Application.IServices;
using MediatR;

namespace AlasimVar.Application.Features.Queries.User;

public class UserListQueryHandler: IRequestHandler<UserListQuery, List<Domain.Entities.User>>
{
    private readonly IUserService _userService;

    public UserListQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<List<Domain.Entities.User>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetUserListAsync();
    }
}