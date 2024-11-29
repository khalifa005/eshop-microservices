using BuildingBlocks.Behaviors;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//before building the app - add services to DI containers

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssemblies(assembly);
  config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
  opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

//Configure HTTP request pipline 
app.MapCarter();

app.UseExceptionHandler(exceptionHandlerApp =>
{
  exceptionHandlerApp.Run(async context => {

    var exceptoin = context.Features.Get<IExceptionHandlerFeature>()?.Error;
    if (exceptoin == null)
      return;

    var problemDetails = new ProblemDetails
    {
      Title = exceptoin.Message,
      Status = StatusCodes.Status500InternalServerError,
      Detail = exceptoin.StackTrace
    };

    var logger = context.RequestServices.GetService<ILogger<Program>>();

    logger.LogError(exceptoin, exceptoin.Message);

    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    context.Response.ContentType = "application/problem+json";

    context.Response.WriteAsJsonAsync(problemDetails);
});

});

app.Run();
