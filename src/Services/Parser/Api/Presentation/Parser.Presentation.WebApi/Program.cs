using Parser.Core.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.MapGet(
    pattern: "/parserDynamicEntityModels",
    handler: async (IMediator mediator) =>
    {
        var models = await mediator.Send(new GetAllParserDynamicEntityModelsQuery());
        return Results.Ok(models);
    });

app.MapGet(
    pattern: "/parserDynamicEntityModels/{id}", 
    handler: async (object id, IMediator mediator) =>
    {
        var model = await mediator.Send(new GetParserDynamicEntityModelByIdQuery() { Id = id });
        return Results.Ok(model);
    });

app.MapPost(
    pattern: "/parserDynamicEntityModel",
    handler: async ([FromBody] ParserDynamicEntityModel model, IMediator mediator) =>
    {
        await mediator.Send(new CreateParserDynamicEntityModelCommand() { Model = model });
        return Results.Created($"/parserDynamicEntityModel/{model.Id}", model);
    });

app.MapPost(
    pattern: "/parserDynamicEntityModels",
    handler: async ([FromBody] List<ParserDynamicEntityModel> models, IMediator mediator) =>
    {
        await mediator.Send(new CreateManyParserDynamicEntityModelsCommand() { Models = models });
        return Results.Created($"/parserDynamicEntityModel/{models[0].Id}", models);
    });

app.MapPut(
    pattern: "/parserDynamicEntityModels",
    handler: async ([FromBody] ParserDynamicEntityModel model, IMediator mediator) =>
    {
        var resultEntity = await mediator.Send(new ReplaceOneParserDynamicEntityModelCommand() { Model = model });
        return Results.Ok(resultEntity);
    });

app.MapDelete(
    pattern: "/parserDynamicEntityModels/{id}",
    handler: async (string id, IMediator mediator) =>
    {
        await mediator.Send(new DeleteParserDynamicEntityModelByIdCommand() { Id = id });
        return Results.NoContent();
    });

app.Run();
