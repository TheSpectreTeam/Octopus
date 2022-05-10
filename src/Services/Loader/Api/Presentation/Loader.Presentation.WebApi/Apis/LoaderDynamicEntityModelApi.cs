namespace Loader.Presentation.WebApi.Apis
{
    public class LoaderDynamicEntityModelApi
    {
        public void Register(WebApplication app)
        {
            app.MapGet(
                pattern: "/loaderDynamicEntityModels", 
                handler: async (IMongoRepository <LoaderDynamicEntityModel> mongoRepository) =>
                    await mongoRepository.GetAllAsync());

            app.MapGet(
                pattern: "/loaderDynamicEntityModels/{id}",
                handler: async (string id, IMongoRepository<LoaderDynamicEntityModel> mongoRepository) =>
                {
                    var objectId = new ObjectId(id);
                    return await mongoRepository.GetByIdAsync(objectId) is LoaderDynamicEntityModel entity
                    ? Results.Ok(entity)
                    : Results.NotFound();
                });

            app.MapPost(
                pattern: "/loaderDynamicEntityModels",
                handler: async ([FromBody] LoaderDynamicEntityModel entity, IMongoRepository<LoaderDynamicEntityModel> mongoRepository) =>
                {
                    await mongoRepository.CreateAsync(entity);
                    return Results.Created($"/loaderDynamicEntityModels/{entity.Id}", entity);
                });

            app.MapPut(
                pattern: "/loaderDynamicEntityModels",
                handler: async ([FromBody] LoaderDynamicEntityModel entity, IMongoRepository<LoaderDynamicEntityModel> mongoRepository) =>
                {
                    var resultEntity = await mongoRepository.ReplaceOneAsync(entity);
                    return Results.Ok(resultEntity);
                });

            app.MapDelete(
                pattern: "/loaderDynamicEntityModels", 
                handler: async (string id, IMongoRepository<LoaderDynamicEntityModel> mongoRepository) =>
                {
                    var objectId = new ObjectId(id);
                    await mongoRepository.DeleteByIdAsync(objectId);
                    return Results.NoContent();
                });
        }
    }
}
