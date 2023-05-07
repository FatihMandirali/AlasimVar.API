using AlasimVar.Application.Exceptions;
using AlasimVar.Application.IServices;
using AlasimVar.Domain.Entities;
using AlasimVar.Domain.EntityEnums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace AlasimVar.Application.Features.Commands.Register;

public class RegisterCommandHandler: IRequestHandler<RegisterCommand, object>
{
    private readonly IUserService _userService;

    public RegisterCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<object> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.FindBy(x => x.Email == request.Email).AsNoTracking().FirstOrDefaultAsync();
        if (user is not null)
            throw new ErrorException("email_already_exist");
        user = new User();
        user.Email = request.Email;
        user.Name = request.Name;
        user.Surname = request.Surname;
        user.City = request.City;
        user.Couty = request.County;
        user.Password = BC.HashPassword(request.Password);
        user.Role = RolesEnum.User;
        user.Phone = request.Phonenumber;
        await _userService.AddAsync(user);
        return null;
    }
}