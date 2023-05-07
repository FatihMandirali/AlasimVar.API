using System.Globalization;
using AlasimVar.API.Filters;
using AlasimVar.API.Middleware;
using AlasimVar.Application.Enums;
using AlasimVar.Application.Features.Commands.Login;
using AlasimVar.Application.Helpers.Jwt;
using AlasimVar.Application.IServices;
using AlasimVar.Domain;
using AlasimVar.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AlasimVar.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ServiceCollectionExtension(this IServiceCollection services,
        IConfiguration configuration)
    {
        #region Swagger
        services.AddSwaggerGen();
        var securityScheme = new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JSON Web Token based security",
        };

        var securityReq = new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        };

        var contact = new OpenApiContact()
        {
            Name = "AlasimVar",
            Email = "fatih.mandirali@hotmail.com",
            Url = new Uri("http://www.mohamadlawand.com")
        };

        var license = new OpenApiLicense()
        {
            Name = "Free License",
            Url = new Uri("http://www.mohamadlawand.com")
        };

        var info = new OpenApiInfo()
        {
            Version = "v1",
            Title = "AlasimVar - JWT Authentication with Swagger",
            Description = "AlasimVar - JWT Authentication with Swagger",
            TermsOfService = new Uri("http://www.example.com"),
            Contact = contact,
            License = license
        };

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("v1", info);
            o.AddSecurityDefinition("Bearer", securityScheme);
            o.AddSecurityRequirement(securityReq);
        });

        #endregion
        #region Default
        services.AddControllers(options =>
        {
            options.Filters.Add(new HttpResponseExceptionFilter());
            options.Filters.Add(typeof(ValidateModelStateAttribute));
        }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
        #endregion
        #region FluentValidation
        services.AddFluentValidation(conf =>
        {
            conf.RegisterValidatorsFromAssembly(typeof(ProcessStatusEnum).Assembly);
        });
        #endregion
        #region Services
        services.AddScoped<ExceptionCatcherMiddleware>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenHelper, JwtHelper>();
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
        #region Multi-Language

        services.AddLocalization();

        services.Configure<RequestLocalizationOptions>(
            options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("tr")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "tr", uiCulture: "tr");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new[]
                    { new RouteDataRequestCultureProvider { IndexOfCulture = 1, IndexofUICulture = 1 } };
            });


        #endregion
        
        return services;
    }
}