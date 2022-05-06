using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Repository.MongoDb
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterMongoDbRepository(this IServiceCollection services, IConfiguration configuration)
            => services
            .Configure<MongoDbConfiguration>(configuration.GetSection(nameof(MongoDbConfiguration)))
            .AddSingleton<IMongoDbConfiguration>(serviceProvider => 
                serviceProvider.GetRequiredService<IOptions<MongoDbConfiguration>>().Value)
            .AddScoped(typeof(IMongoDbContext<>), typeof(MongoDbContext<>))
            .AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
    }
}
