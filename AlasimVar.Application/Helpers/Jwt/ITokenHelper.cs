using AlasimVar.Domain.EntityEnums;

namespace AlasimVar.Application.Helpers.Jwt;

public interface ITokenHelper
{
    AccessToken CreateToken(RolesEnum rolesEnum, int id);
}