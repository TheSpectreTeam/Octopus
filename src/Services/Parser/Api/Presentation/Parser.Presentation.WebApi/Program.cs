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
    handler: async ([FromBody] List<ParserDynamicEntityModel> models, IMediator mediator, CancellationToken cancellationToken) =>
    {
        await mediator.Send(
            request: new CreateManyParserDynamicEntityModelsCommand() { Models = models },
            cancellationToken: cancellationToken);

        return Results.Created($"api/parserDynamicEntityModel/{models[0].Id}", models);
    });

app.MapPut(
    pattern: "api/parserDynamicEntityModels",
    handler: async ([FromBody] ParserDynamicEntityModel model, IMediator mediator, CancellationToken cancellationToken) =>
    {
        var resultEntity = await mediator.Send(
            request: new ReplaceOneParserDynamicEntityModelCommand() { Model = model },
            cancellationToken: cancellationToken);

        return Results.Ok(resultEntity);
    });

app.MapDelete(
    pattern: "api/parserDynamicEntityModels/{id}",
    handler: async (string id, IMediator mediator, CancellationToken cancellationToken) =>
    {
        await mediator.Send(
            request: new DeleteParserDynamicEntityModelByIdCommand() { Id = id },
            cancellationToken: cancellationToken);

        return Results.NoContent();
    });

app.Run();
