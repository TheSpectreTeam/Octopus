using System.Reflection;
using Loader.Core.Application.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace Loader.Core.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationInfrastructure(this IServiceCollection services)
            => services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
    }
}
