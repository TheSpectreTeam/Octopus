namespace Parser.FunctionalTests.ApiTests
{
    public class TestParserApi : WebApplicationFactory<Program>
    {
        private readonly string _environment;
        private readonly Action<IServiceCollection>? _serviceOverride;

        public TestParserApi(
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
