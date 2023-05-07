using Catalog.API.Extensions;
using Catalog.API.Middleware;
using Catalog.Application.DI;
using Catalog.Infostructure.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(x => x.WithOrigins("http://localhost:50000", "http://localhost:3000")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
});

builder.Services.ApplicationConfigure();
builder.Services.InfostructureConfigure(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

await app.MigrateDatabaseAsync();
await app.SeedDataAsync();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.UseCors();

app.Run();
