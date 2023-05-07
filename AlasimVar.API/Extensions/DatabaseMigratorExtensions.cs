using AlasimVar.Domain;
using AlasimVar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace AlasimVar.API.Extensions;

public static class DatabaseMigratorExtensions
{
    public static async Task DatabaseMigrator(this AlasimVarDbContext dbContext)
    {
        await dbContext.Database.MigrateAsync();
        await SeedDataCreate(dbContext);
    }
    public static async Task SeedDataCreate(AlasimVarDbContext dbContext)
    {
        if (await dbContext.Users.CountAsync() > 0) return;
        var user = new User
        {
            CreateDate = DateTime.Now,
            Email = "fatih.mandirali@hotmail.com",
            IsActive = true,
            IsDeleted = false,
            ModifiedDate = DateTime.Now,
            Name = "Fatih",
            Password = BC.HashPassword("123456"),
            Phone = "5393551932",
            Surname = "Mandıralı",
            City = "Sakarya",
            Couty = "Erenler",
        };
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        

    }
}