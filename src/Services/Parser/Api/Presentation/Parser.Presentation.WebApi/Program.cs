var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
