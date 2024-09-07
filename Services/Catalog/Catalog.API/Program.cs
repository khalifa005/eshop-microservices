var builder = WebApplication.CreateBuilder(args);

//before building the app - add services to DI containers
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

var app = builder.Build();

//Configure HTTP request pipline 
app.MapCarter();

app.Run();
