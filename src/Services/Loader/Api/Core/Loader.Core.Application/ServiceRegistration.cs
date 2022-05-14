using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Loader.Core.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationInfrastructure(this IServiceCollection services)
            => services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
