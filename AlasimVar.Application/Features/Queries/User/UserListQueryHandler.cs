using AlasimVar.Application.IServices;
using MediatR;

namespace AlasimVar.Application.Features.Queries.User;

public class UserListQueryHandler: IRequestHandler<UserListQuery, List<Domain.Entities.User>>
{
    private readonly IUserService _userService;
    private readonly IElasticsearchService _elasticsearchService;

    public UserListQueryHandler(IUserService userService, IElasticsearchService elasticsearchService)
    {
        _userService = userService;
        _elasticsearchService = elasticsearchService;
    }

    public async Task<List<Domain.Entities.User>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        //await _elasticsearchService.ChekIndex("product");
        var userLisr = new List<Domain.Entities.User>();
        /*userLisr.Add(new Domain.Entities.User
        {
            Id = 1,
            Name = "fatih",
            Surname =  "mandıralı",
            Password = "1"
        });
        userLisr.Add(new Domain.Entities.User
        {
            Id = 2,
            Name = "fatih1",
            Surname =  "mandıralı1",
            Password = "2"
        });*/
        //await _elasticsearchService.InsertDocuments("product", userLisr);
        
        var res = await _elasticsearchService.GetDocuments("product");
        return res;
        //return await _userService.GetUserListAsync();
    }
}