namespace Loader.Presentation.WebApi.Apis
{
    public class LoaderDynamicEntityModelApi
    {
        public void Register(WebApplication app)
        {
            app.MapPost(
                pattern: "/loaderDynamicEntityModels",
                handler: async ([FromBody] CreateDynamicEntityCommand command, IMediator mediator, CancellationToken cancellationToken) 
                            => Results.Ok(await mediator.Send(command, cancellationToken)));

            app.MapDelete(
                pattern: "/loaderDynamicEntityModels",
                handler: async (string id, IMediator mediator, CancellationToken cancellationToken)
                            => Results.Ok(await mediator.Send(new DeleteDynamicEntityCommand { Id = id }, cancellationToken)));
        }
    }
}
