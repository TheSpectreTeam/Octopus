var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ConfigureSwagger();
}

app.UseErrorHandlingMiddleware();

app.MapDynamicEntityEndpoints();

app.Run();
