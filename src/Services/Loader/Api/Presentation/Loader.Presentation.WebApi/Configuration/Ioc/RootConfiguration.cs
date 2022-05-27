using Loader.Core.Application;

namespace Loader.Presentation.WebApi.Configuration.Ioc
{
    public static class RootConfiguration
    {
        public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
            => services
                .RegisterMongoDbRepository(configuration)
                .AddApplicationInfrastructure()
                .AddEndpointsApiExplorer()
                .RegisterSwagger();
    }
}
