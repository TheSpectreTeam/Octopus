namespace Loader.Presentation.WebApi.Endpoints
{
    public static class DynamicEntityEndpoints
    {
        public static void MapDynamicEntityEndpoints(this WebApplication app)
        {
            app.MapGet(
                pattern: "/api/dynamicEntity",
                handler: DynamicEntityEndpointsMethods.GetAllEntitiesAsync)
                .Produces<Response<IEnumerable<LoaderDynamicEntity>>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status204NoContent)
                .WithName("GetAllEntities")
                .WithTags("EntityQueries");

            app.MapGet(
                pattern: "/api/dynamicEntity/{id}",
                handler: DynamicEntityEndpointsMethods.GetEntityById)
                .Produces<Response<LoaderDynamicEntity>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("GetEntityById")
                .WithTags("EntityQueries");

            app.MapPost(
                pattern: "/api/dynamicEntity",
                handler: DynamicEntityEndpointsMethods.AddNewEntity)
                .Produces(StatusCodes.Status201Created)
                .WithName("AddNewEntity")
                .WithTags("EntityCommands");

            app.MapPut(
                pattern: "/api/dynamicEntity",
                handler: DynamicEntityEndpointsMethods.UpdateEntity)
                .Produces<Response<LoaderDynamicEntity>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("UpdateEntity")
                .WithTags("EntityCommands");

            app.MapDelete(
                pattern: "/api/dynamicEntity/{id}",
                handler: DynamicEntityEndpointsMethods.DeleteEntity)
                .Produces(StatusCodes.Status204NoContent)
                .WithName("DeleteEntity")
                .WithTags("EntityCommands");
        }
    }
}
