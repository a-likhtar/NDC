using Microsoft.EntityFrameworkCore;
using NDC.BackgroundJob;
using NDC.DataAccess;
using NDC.DataAccess.Repositories;
using NDC.Domain.Interfaces;
using NDC.Domain.Services;
using NDC.Domain.Services.Implementations;
using Quartz;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<DataCollectorBackgroundService>();

// registering Quartz
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
});

// Registering DBContext
builder.Services.AddDbContext<MeteoritesContext>(optionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("MeteoritesContext");
    optionsBuilder.UseSqlServer(connectionString);
});

builder.Services.AddHttpClient<IItemSyncService, ItemSyncService>();
// builder.Services.AddScoped<IItemSyncService, ItemSyncService>();
builder.Services.AddScoped<IMeteoriteClassRepository, MeteoriteClassRepository>();
builder.Services.AddScoped<IMeteoriteRepository, MeteoriteRepository>();

var host = builder.Build();
host.Run();