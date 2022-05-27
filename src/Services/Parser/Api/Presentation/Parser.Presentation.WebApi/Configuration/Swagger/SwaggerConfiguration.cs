namespace Parser.Presentation.WebApi.Configuration.Swagger
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
            => services
                    .AddEndpointsApiExplorer()
                    .AddSwaggerGen();

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
            => app
                .UseSwagger()
                .UseSwaggerUI(_ => _.SwaggerEndpoint("/swagger/v1/swagger.json", "Octopus.Parser.API"));
    }
}
