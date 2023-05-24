using AlasimVar.Domain.EntityEnums;
using MediatR;

namespace AlasimVar.Application.Features.Commands.CreateUser;

public class CreateUserCommand: IRequest<object>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string City { get; set; }
    public string Couty { get; set; }
    public RolesEnum Role { get; set; }
}