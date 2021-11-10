using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//add ocelot
builder.Services.AddOcelot()
    .AddCacheManager(settings => settings.WithDictionaryHandle()); ;

 

//configure enviroent files and logging
builder.WebHost
    .ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
})

    .ConfigureLogging((hostingContext, loggingbuilder) =>
{
    loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    loggingbuilder.AddConsole();
    loggingbuilder.AddDebug();
}
);

//app.MapGet("/", () => "Hello World!");
await app.UseOcelot();

app.Run();
