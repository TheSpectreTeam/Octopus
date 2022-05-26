namespace Parser.Presentation.WebApi.Endpoints
{
    public static class DynamicEntityEndpoints
    {
        public static void MapDynamicEntityEndpoints(this WebApplication app)
        {
            app.MapGet(
                pattern: "api/parserDynamicEntityModels",
                handler: async (IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var models = await mediator.Send(
                        request: new GetAllParserDynamicEntityModelsQuery(),
                        cancellationToken: cancellationToken);

                    return Results.Ok(models);
                })
                .Produces<Response<IEnumerable<ParserDynamicEntityModel>>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status204NoContent)
                .WithName("GetAllEntities")
                .WithTags("EntityQueries");

            app.MapGet(
                pattern: "api/parserDynamicEntityModels/{id}",
                handler: async (string id, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var model = await mediator.Send(
                        request: new GetParserDynamicEntityModelByIdQuery() { Id = id },
                        cancellationToken: cancellationToken);

                    return Results.Ok(model);
                })
                .Produces<Response<ParserDynamicEntityModel>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("GetEntityById")
                .WithTags("EntityQueries");

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
                })
                .Produces(StatusCodes.Status201Created)
                .WithName("AddNewEntity")
                .WithTags("EntityCommands");

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
                })
                .Produces(StatusCodes.Status201Created)
                .WithName("AddNewEntities")
                .WithTags("EntityCommands");

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
                })
                .Produces<Response<ParserDynamicEntityModel>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("UpdateEntity")
                .WithTags("EntityCommands");

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
                })
                .Produces(StatusCodes.Status204NoContent)
                .WithName("DeleteEntity")
                .WithTags("EntityCommands");
        }
    }
}
