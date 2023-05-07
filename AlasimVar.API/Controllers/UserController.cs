using AlasimVar.Application.Features.Queries.User;
using AlasimVar.Application.IServices;
using AlasimVar.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlasimVar.API.Controllers;

public class UserController:BaseController
{
    private readonly IMediator _mediator;
    private readonly IUserService _userService;

    public UserController(IUserService userService, IMediator mediator)
    {
        _userService = userService;
        _mediator = mediator;
    }
    
    [HttpGet("List")]
    public async Task<List<User>> GetUserList([FromQuery] UserListQuery userListQuery)
    {
        var response = await _mediator.Send(userListQuery);
        return response;
    }
}