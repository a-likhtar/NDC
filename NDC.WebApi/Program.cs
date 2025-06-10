using Microsoft.EntityFrameworkCore;
using NDC.DataAccess;
using NDC.DataAccess.Repositories;
using NDC.Domain.Interfaces;
using NDC.Domain.Services;
using NDC.Domain.Services.Implementations;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Registering DBContext
builder.Services.AddDbContext<MeteoritesContext>(optionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("MeteoritesContext");
    optionsBuilder.UseSqlServer(connectionString);
});

builder.Services.AddHttpClient<IItemSyncService, ItemSyncService>();
builder.Services.AddScoped<IItemSyncService, ItemSyncService>();
builder.Services.AddScoped<IMeteoriteClassRepository, MeteoriteClassRepository>();
builder.Services.AddScoped<IMeteoriteRepository, MeteoriteRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Регистрация DbContext и репозиториев
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(connectionString));
//
// builder.Services.AddScoped<IUserRepository, UserRepository>();