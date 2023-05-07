using AlasimVar.Application.IServices;
using AlasimVar.Application.Repository;
using AlasimVar.Domain;
using AlasimVar.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlasimVar.Infrastructure.Services;

public class UserService:Repository<User>,IUserService
{
    public UserService(AlasimVarDbContext context) : base(context)
    {
    }

    public async Task<List<User>> GetUserListAsync()
    {
        var userList = await FindBy(x => x.IsActive && !x.IsDeleted).AsNoTracking().ToListAsync();
        return userList;
    }
}