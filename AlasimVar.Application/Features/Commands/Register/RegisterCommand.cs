using MediatR;

namespace AlasimVar.Application.Features.Commands.Register;

public class RegisterCommand: IRequest<object>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phonenumber { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string County { get; set; }
}