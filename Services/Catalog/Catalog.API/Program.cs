var builder = WebApplication.CreateBuilder(args);
//before building the app - add services to DI containers

var app = builder.Build();
//Configure HTTP request pipline 


app.Run();
