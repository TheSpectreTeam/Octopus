namespace Loader.Presentation.WebApi.Configuration.Middlewares
{
    public static class MiddlewaresConfiguration
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder app)
            => app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
