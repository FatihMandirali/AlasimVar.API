using AlasimVar.Application.Enums;
using AlasimVar.Application.Features.Commands.Login;
using AlasimVar.Application.Features.Commands.Register;
using AlasimVar.Application.Features.Queries.User;
using AlasimVar.Application.Helpers.Jwt;
using AlasimVar.Application.Models.BaseModel;
using AlasimVar.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlasimVar.API.Controllers;

public class AuthenticationController:BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(IMediator mediator, ILogger<AuthenticationController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    /// <summary>
    /// Login Service
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("Login")]
    public async Task<BaseResponse<AccessToken>> Login([FromBody] LoginCommand request)
    {
        _logger.LogError($"{request.Email} {request.Password} login errorrrr");
        var response = await _mediator.Send(request);
        return new BaseResponse<AccessToken>(ProcessStatusEnum.Success,null,response);
    }
    
    [HttpPost("Register")]
    public async Task<BaseResponse<object>> Register([FromBody] RegisterCommand request)
    {
        _logger.LogInformation("Register informatrion : test");
        var response = await _mediator.Send(request);
        return new BaseResponse<object>(ProcessStatusEnum.Success,null,response);
    }
}