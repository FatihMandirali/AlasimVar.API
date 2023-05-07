using AlasimVar.Application.Exceptions;
using AlasimVar.Application.Helpers.Jwt;
using AlasimVar.Application.IServices;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace AlasimVar.Application.Features.Commands.Login;


public class LoginCommandHandler: IRequestHandler<LoginCommand, AccessToken>
{
    private readonly ITokenHelper _tokenHelper;
    private readonly IUserService _userService;

    public LoginCommandHandler(ITokenHelper tokenHelper, IUserService userService)
    {
        _tokenHelper = tokenHelper;
        _userService = userService;
    }
    public async Task<AccessToken> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.FindAsync(x=>x.Email == request.Email);
        if (user is null)
            throw new ErrorException("login_info_wrong");
        var verify = BC.Verify(request.Password, user.Password);
        if (!verify)
            throw new ErrorException("login_info_wrong");
        var token = _tokenHelper.CreateToken(user.Role, user.Id);
        return token;
    }
}