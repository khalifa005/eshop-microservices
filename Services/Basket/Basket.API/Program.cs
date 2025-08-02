using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Carter;
using FluentValidation;
using Marten;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var assebly = typeof(Program).Assembly;

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CacheBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
  options.Configuration = builder.Configuration.GetConnectionString("Redis")!;
  //options.InstanceName = "Basket";
  //options.InstanceName = "BasketInstance";
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssembly(assebly);
  config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
  config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});


builder.Services.AddMarten(opt =>
{
  opt.Connection(builder.Configuration.GetConnectionString("Database")!);
  opt.Schema.For<ShoppingCart>()
      .Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddHealthChecks()
  .AddNpgSql(builder.Configuration.GetConnectionString("Database"))
  .AddRedis(builder.Configuration.GetConnectionString("Redis"))
  ;

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health",
  new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
  {
    ResponseWriter = HealthChecks.UI.Client.UIResponseWriter.WriteHealthCheckUIResponse
  });
app.Run();
