using System.Reflection;
using AlasimVar.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlasimVar.Domain;

public class AlasimVarDbContext: DbContext
{
    public AlasimVarDbContext(DbContextOptions<AlasimVarDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}