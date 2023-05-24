using AlasimVar.Application.Features.Commands.CreateUser;
using AlasimVar.Application.Features.Queries.User;
using AlasimVar.Application.Features.Queries.UserFindById;
using AlasimVar.Application.IServices;
using AlasimVar.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlasimVar.API.Controllers;

public class UserController:BaseController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("List")]
    public async Task<List<User>> GetUserList([FromQuery] UserListQuery userListQuery)
    {
        var response = await _mediator.Send(userListQuery);
        return response;
    }
    [HttpGet("UserById")]
    public async Task<User> GetUserById([FromQuery] UserFindByIdQuery userFindByIdQuery)
    {
        var response = await _mediator.Send(userFindByIdQuery);
        return response;
    }
    [HttpPost("CreateUser")]
    public async Task<object> PostUser([FromBody] CreateUserCommand createUserCommand)
    {
        var response = await _mediator.Send(createUserCommand);
        return response;
    }
}