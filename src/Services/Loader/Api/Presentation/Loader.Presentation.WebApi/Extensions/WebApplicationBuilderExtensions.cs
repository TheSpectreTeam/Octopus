namespace Loader.Presentation.WebApi.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static IServiceCollection ConfigureServices(this WebApplicationBuilder builder)
            => builder
                .Services
                    .Configure(builder.Configuration);
    }
}
