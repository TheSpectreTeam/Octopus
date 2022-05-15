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
                handler: async (IMediator mediator, CancellationToken cancellationToken) 
                            => Results.Ok(await mediator.Send(new GetAllDynamicEntitiesQuery(), cancellationToken)));

            app.MapGet(
                pattern: "/api/dynamicEntity/{id}",
                handler: async (string id, IMediator mediator, CancellationToken cancellationToken)
                            => Results.Ok(await mediator.Send(new GetDynamicEntityQuery { Id = id }, cancellationToken)));

            app.MapPost(
                pattern: "/api/dynamicEntity",
                handler: async ([FromBody] CreateDynamicEntityCommand command, IMediator mediator, CancellationToken cancellationToken) 
                            => Results.Ok(await mediator.Send(command, cancellationToken)));

            app.MapPut(
                pattern: "/api/dynamicEntity",
                handler: async ([FromBody] UpdateDynamicEntityCommand command, IMediator mediator, CancellationToken cancellationToken)
                            => Results.Ok(await mediator.Send(command, cancellationToken)));

            app.MapDelete(
                pattern: "/api/dynamicEntity/{id}",
                handler: async (string id, IMediator mediator, CancellationToken cancellationToken)
                            => Results.Ok(await mediator.Send(new DeleteDynamicEntityCommand { Id = id }, cancellationToken)));
        }
    }
}
