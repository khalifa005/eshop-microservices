using BuildingBlocks.Behaviors;
using FluentValidation;

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

app.Run();
