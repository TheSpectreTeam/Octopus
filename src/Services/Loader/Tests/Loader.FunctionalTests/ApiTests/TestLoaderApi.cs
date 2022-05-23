namespace Loader.FunctionalTests.ApiTests
{
    internal class TestLoaderApi : WebApplicationFactory<Program>
    {
        private readonly string _environment;
        private readonly Action<IServiceCollection>? _serviceOverride;

        public TestLoaderApi(
            Action<IServiceCollection> serviceOverride,
            string environment = "Development")
        {
            _serviceOverride = serviceOverride;
            _environment = environment;
        }

        protected override IHost CreateHost(IHostBuilder hostBuilder)
        {
            if (_serviceOverride is not null)
            {
                hostBuilder.UseEnvironment(_environment);
                hostBuilder.ConfigureServices(_serviceOverride);
            }
            return base.CreateHost(hostBuilder);
        }
    }
}