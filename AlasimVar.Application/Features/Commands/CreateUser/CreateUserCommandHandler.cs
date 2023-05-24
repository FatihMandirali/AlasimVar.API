using AlasimVar.Application.Helpers.Jwt;
using AlasimVar.Application.IServices;
using AlasimVar.Domain.Entities;
using MediatR;

namespace AlasimVar.Application.Features.Commands.CreateUser;

public class CreateUserCommandHandler: IRequestHandler<CreateUserCommand, object>
{
    private readonly IElasticsearchService _elasticsearchService;

    public CreateUserCommandHandler(IElasticsearchService elasticsearchService)
    {
        _elasticsearchService = elasticsearchService;
    }

    public async Task<object> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User();
        user.Id = request.Id;
        user.City = request.City;
        user.Couty = request.Couty;
        user.Phone = request.Phone;
        user.Email = request.Email;
        user.Name = request.Name;
        user.Password = request.Password;
        user.Surname = request.Surname;
        await _elasticsearchService.InsertDocument("product", user);
        return null;
    }
}