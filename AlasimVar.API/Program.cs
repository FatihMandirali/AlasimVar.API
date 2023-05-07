using AlasimVar.API.Extensions;
using AlasimVar.Domain;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
builder.Services.ServiceCollectionExtension(builder.Configuration);

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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();