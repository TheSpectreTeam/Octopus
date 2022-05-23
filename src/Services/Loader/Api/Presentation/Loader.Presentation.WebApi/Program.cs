var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ConfigureSwagger();
}

app.UseErrorHandlingMiddleware();
app.MapDynamicEntityEndpoints();

app.Run();
