using AlasimVar.Application.Helpers.Jwt;
using MediatR;

namespace AlasimVar.Application.Features.Commands.Login;

public class LoginCommand: IRequest<AccessToken>
{
    public string Email { get; set; }
    public string Password { get; set; }
}