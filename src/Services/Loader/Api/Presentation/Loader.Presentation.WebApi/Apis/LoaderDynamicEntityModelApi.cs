using Loader.Core.Application.Features.DynamicEntity.Commands.CreateDynamicEntity;
using Loader.Core.Application.Features.DynamicEntity.Commands.DeleteDynamicEntity;
using Loader.Core.Application.Features.DynamicEntity.Commands.UpdateDynamicEntity;
using Loader.Core.Application.Features.DynamicEntity.Queries.GetAllDynamicEntities;
using Loader.Core.Application.Features.DynamicEntity.Queries.GetDynamicEntityById;

namespace Loader.Presentation.WebApi.Apis
{
    public class LoaderDynamicEntityModelApi
    {
        public void Register(WebApplication app)
        {
            app.MapGet(
                pattern: "/api/dynamicEntity",
                handler: async (IMediator mediator, CancellationToken cancellationToken) => 
                    await mediator.Send(
                        request: new GetAllDynamicEntitiesQuery(),
                        cancellationToken: cancellationToken))
                .Produces<Response<IEnumerable<LoaderDynamicEntity>>>(StatusCodes.Status200OK)
                .WithName("GetAllEntities")
                .WithTags("EntityQueries");

            app.MapGet(
                pattern: "/api/dynamicEntity/{id}",
                handler: async (string id, IMediator mediator, CancellationToken cancellationToken) =>
                    await mediator.Send(
                        request: new GetDynamicEntityQuery { Id = id },
                        cancellationToken: cancellationToken) is Response<LoaderDynamicEntity> entity
                            ? Results.Ok(entity)
                            : Results.NotFound())
                .Produces<Response<LoaderDynamicEntity>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("GetEntityById")
                .WithTags("EntityQueries");

            app.MapPost(
                pattern: "/api/dynamicEntity",
                handler: async ([FromBody] CreateDynamicEntityCommand command, IMediator mediator, CancellationToken cancellationToken) =>
                    await mediator.Send(
                        request: command, 
                        cancellationToken: cancellationToken))
                .Produces(StatusCodes.Status201Created)
                .WithName("AddNewEntity")
                .WithTags("EntityCommands"); 

            app.MapPut(
                pattern: "/api/dynamicEntity",
                handler: async ([FromBody] UpdateDynamicEntityCommand command, IMediator mediator, CancellationToken cancellationToken) => 
                    await mediator.Send(
                        request: command, 
                        cancellationToken: cancellationToken) is Response<LoaderDynamicEntity> entity
                    ? Results.Ok(entity)
                    : Results.NotFound())
                .Produces<Response<LoaderDynamicEntity>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("UpdateEntity")
                .WithTags("EntityCommands");

            app.MapDelete(
                pattern: "/api/dynamicEntity/{id}",
                handler: async (string id, IMediator mediator, CancellationToken cancellationToken) => 
                    await mediator.Send(
                        request: new DeleteDynamicEntityCommand { Id = id }, 
                        cancellationToken: cancellationToken))
                .Produces(StatusCodes.Status204NoContent)
                .WithName("DeleteEntity")
                .WithTags("EntityCommands");
        }
    }
}
