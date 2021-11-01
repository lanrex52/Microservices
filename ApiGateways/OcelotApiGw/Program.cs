using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


//add ocelot
builder.Services.AddOcelot();



//configure logging
builder.WebHost.ConfigureLogging((hostingContext, loggingbuilder) =>
{
    loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    loggingbuilder.AddConsole();
    loggingbuilder.AddDebug();
}
);

//app.MapGet("/", () => "Hello World!");
await app.UseOcelot();

app.Run();
