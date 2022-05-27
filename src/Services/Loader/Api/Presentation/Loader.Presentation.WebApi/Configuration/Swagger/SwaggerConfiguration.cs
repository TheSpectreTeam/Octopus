namespace Loader.Presentation.WebApi.Configuration.Swagger
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
            => services
                .AddSwaggerGen();

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
            => app
                .UseSwagger()
                .UseSwaggerUI(_=>_.SwaggerEndpoint("/swagger/v1/swagger.json", "Octopus.Loader.API"));
    }
}
