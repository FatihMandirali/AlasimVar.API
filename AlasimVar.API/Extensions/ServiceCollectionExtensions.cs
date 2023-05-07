using AlasimVar.Application.IServices;
using AlasimVar.Domain;
using AlasimVar.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace AlasimVar.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ServiceCollectionExtension(this IServiceCollection services,
        IConfiguration configuration)
    {
        #region Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        #endregion
        #region Default
        services.AddControllers();
        #endregion
        #region Services
        services.AddScoped<IUserService, UserService>();
        #endregion
        #region PostgreSql

        services.AddDbContext<AlasimVarDbContext>(options => options.UseNpgsql(
            configuration.GetConnectionString("SqlConnection"), npgOptions =>
                npgOptions.MigrationsAssembly("AlasimVar.Domain")
        ));

        #endregion
        #region Mediatr
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(AlasimVar.Application.Features.Queries.User.UserListQuery)));
        #endregion
        
        return services;
    }
}