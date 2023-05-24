using AlasimVar.Application.IServices;
using Elasticsearch.Net;
using MediatR;
using Nest;

namespace AlasimVar.Application.Features.Queries.UserFindById;

public class UserFindByIdQueryHandler: IRequestHandler<UserFindByIdQuery, Domain.Entities.User>
{
    private readonly IElasticsearchService _elasticsearchService;

    public UserFindByIdQueryHandler(IElasticsearchService elasticsearchService)
    {
        _elasticsearchService = elasticsearchService;
    }

    public async Task<Domain.Entities.User> Handle(UserFindByIdQuery request, CancellationToken cancellationToken)
    {
        //var query = new TermQuery { Field = "id", Value = request.Id};
        var user = await _elasticsearchService.GetDocument("product", request.Id);
        return user;
    }
}