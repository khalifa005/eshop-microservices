

using Catalog.API.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//before building the app - add services to DI containers

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssemblies(assembly);
  config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
  config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
  opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
  builder.Services.InitializeMartenWith<CatalogInitialData>();
}


builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
  .AddNpgSql(builder.Configuration.GetConnectionString("Database"));

var app = builder.Build();

//Configure HTTP request pipline 
app.MapCarter();
//empty option for using cusom handler
app.UseExceptionHandler(options=> { });

app.UseHealthChecks("/health", new HealthCheckOptions
{
  ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

app.Run();
