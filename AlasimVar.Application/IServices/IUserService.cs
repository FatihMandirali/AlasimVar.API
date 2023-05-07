using AlasimVar.Application.Repository;
using AlasimVar.Domain.Entities;

namespace AlasimVar.Application.IServices;

public interface IUserService:IRepository<User>
{
    Task<List<User>> GetUserListAsync();
}