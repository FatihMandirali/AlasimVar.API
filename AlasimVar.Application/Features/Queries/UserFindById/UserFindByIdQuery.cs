using MediatR;

namespace AlasimVar.Application.Features.Queries.UserFindById;

public class UserFindByIdQuery: IRequest<Domain.Entities.User>
{
    public int Id { get; set; }
}