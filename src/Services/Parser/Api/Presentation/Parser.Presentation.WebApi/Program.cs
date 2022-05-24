var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.MapGet(
    pattern: "api/parserDynamicEntityModels",
    handler: async (IMediator mediator, CancellationToken cancellationToken) =>
    {
        var models = await mediator.Send(
            request: new GetAllParserDynamicEntityModelsQuery(),
            cancellationToken: cancellationToken);

        return Results.Ok(models);
    });

app.MapGet(
    pattern: "api/parserDynamicEntityModels/{id}", 
    handler: async (object id, IMediator mediator, CancellationToken cancellationToken) =>
    {
        var model = await mediator.Send(
            request: new GetParserDynamicEntityModelByIdQuery() { Id = id },
            cancellationToken: cancellationToken);

        return Results.Ok(model);
    });

app.MapPost(
    pattern: "api/parserDynamicEntityModel",
    handler: async ([FromBody] CreateParserDynamicEntityModelCommand command, 
        IMediator mediator, 
        CancellationToken cancellationToken) =>
    {
        return await mediator.Send(
            request: command,
            cancellationToken: cancellationToken) is Response<string> response
            ? Results.Created($"api/parserDynamicEntityModel/{response.Data}", response.Data)
            : Results.BadRequest();
    });

app.MapPost(
    pattern: "api/parserDynamicEntityModels",
    handler: async ([FromBody] CreateManyParserDynamicEntityModelsCommand command, 
        IMediator mediator, 
        CancellationToken cancellationToken) =>
    {
        return await mediator.Send(
            request: command,
            cancellationToken: cancellationToken) is Response<IDictionary<int, string>> response
            ? Results.Created($"api/parserDynamicEntityModels/{response.Data}", response.Data)
            : Results.BadRequest();
    });

app.MapPut(
    pattern: "api/parserDynamicEntityModels",
    handler: async ([FromBody] ReplaceOneParserDynamicEntityModelCommand command,
        IMediator mediator, 
        CancellationToken cancellationToken) =>
    {
        var resultEntity = await mediator.Send(
            request: command,
            cancellationToken: cancellationToken);

        return resultEntity.Data != null
            ? Results.Ok(resultEntity)
            : Results.NotFound();
    });

app.MapDelete(
    pattern: "api/parserDynamicEntityModels/{id}",
    handler: async (string id, IMediator mediator, CancellationToken cancellationToken) =>
    {
        var result = await mediator.Send(
            request: new DeleteParserDynamicEntityModelByIdCommand() { Id = id },
            cancellationToken: cancellationToken);

        return result.Data 
            ? Results.NoContent()
            : Results.NotFound();
    });

app.Run();
