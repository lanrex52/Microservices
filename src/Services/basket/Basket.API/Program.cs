using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
//Instance of ICOnfiguratopn
var provider = builder.Services.BuildServiceProvider();

var configuration = provider.GetRequiredService<IConfiguration>();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Basket.API", Version = "v1" });
});
builder.Services.AddStackExchangeRedisCache(options=>
{
    options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1"));

}

app.UseAuthorization();

app.MapControllers();

app.Run();
