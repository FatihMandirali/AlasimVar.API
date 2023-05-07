using AlasimVar.Domain.EntityEnums;

namespace AlasimVar.Domain.Entities;

public class User:BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string City { get; set; }
    public string Couty { get; set; }
    public RolesEnum Role { get; set; }
}