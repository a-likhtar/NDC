using NDC.BackgroundJob;
using Quartz;
using Quartz.Impl;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<DataCollectorBackgroundService>();

// registering Quartz
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
});

var host = builder.Build();
host.Run();

/*
 *
 *var connectionString = context.Configuration.GetConnectionString("Default");

   services.AddDbContext<AppDbContext>(options =>
       options.UseSqlServer(connectionString));

   services.AddScoped<IUserRepository, UserRepository>();
 * 
 */