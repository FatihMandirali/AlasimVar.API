using AlasimVar.API.Extensions;
using AlasimVar.API.Middleware;
using AlasimVar.Domain;
using Elastic.Apm.NetCoreAll;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Sinks.Elasticsearch;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
builder.Services.ServiceCollectionExtension(builder.Configuration);
ConfigureLogging();
builder.Host.UseSerilog();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<AlasimVarDbContext>().DatabaseMigrator();
}
var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizationOptions.Value);

//app.UseHttpsRedirection();
app.UseAllElasticApm(builder.Configuration);
app.UseMiddleware<ExceptionCatcherMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{"alasimvar"}-{DateTime.UtcNow:yyyy-MM}"
    };
}