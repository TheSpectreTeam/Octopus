using Loader.Core.Application.Features.DynamicEntity.Commands.CreateDynamicEntity;
using Loader.Core.Application.Features.DynamicEntity.Commands.DeleteDynamicEntity;
using Loader.Core.Application.Features.DynamicEntity.Commands.UpdateDynamicEntity;
using Loader.Core.Application.Features.DynamicEntity.Queries.GetDynamicEntityById;
using Loader.Core.Application.Features.DynamicEntity.Queries.GetAllDynamicEntities;

namespace Loader.Presentation.WebApi.Endpoints
{
    internal static class DynamicEntityEndpointsMethods
    {
        internal static async Task<IResult> GetAllEntitiesAsync(
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(
                request: new GetAllDynamicEntitiesQuery(),
                cancellationToken: cancellationToken);

            return result.Data.Count.Equals(0)
                ? Results.NoContent()
                : Results.Ok(result);
        }

        internal static async Task<IResult> GetEntityById(
            string id,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(
                request: new GetDynamicEntityQuery { Id = id },
                cancellationToken: cancellationToken);

            return result.Data is not null
                ? Results.Ok(result)
                : Results.NotFound();
        }

        internal static async Task<IResult> AddNewEntity(
            [FromBody] CreateDynamicEntityCommand command,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(
                request: command,
                cancellationToken: cancellationToken) is Response<string> response
                ? Results.Created($"/api/dynamicEntity/{response.Data}", response.Data)
                : Results.BadRequest();
        }

        internal static async Task<IResult> UpdateEntity(
            [FromBody] UpdateDynamicEntityCommand command,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(
                request: command,
                cancellationToken: cancellationToken);

            return result.Data is not null
                ? Results.Ok(result)
                : Results.NotFound();
        }

        internal static async Task<IResult> DeleteEntity(
            string id,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(
                request: new DeleteDynamicEntityCommand { Id = id },
                cancellationToken: cancellationToken);

            return result.Data
                ? Results.NoContent()
                : Results.NotFound();
        }
    }
}
