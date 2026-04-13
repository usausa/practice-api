namespace Example.Tests;

using Example.Mock;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

public sealed class ApiApplicationFactory : WebApplicationFactory<Program>
{
    private ITestOutputHelper? testOutputHelper;

    //--------------------------------------------------------------------------------
    // Setting
    //--------------------------------------------------------------------------------

    public void UseTestOutput(ITestOutputHelper? value)
    {
        testOutputHelper = value;
    }

    //--------------------------------------------------------------------------------
    // Override
    //--------------------------------------------------------------------------------

    protected override IHost CreateHost(IHostBuilder builder)
    {
        // Configuration
        builder.ConfigureHostConfiguration(config =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                // TODO
            });
        });

        return base.CreateHost(builder);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Services
        builder.ConfigureServices(services =>
        {
            // Logger
            services.AddSingleton<ILoggerFactory>(_ =>
            {
                var providers = new List<ILoggerProvider>();
                if (testOutputHelper is not null)
                {
                    providers.Add(new XunitLoggerProvider(testOutputHelper, Filter));
                }

                return new LoggerFactory(providers);

                static bool Filter(string category, LogLevel level) =>
                    category.StartsWith("Example", StringComparison.Ordinal) ||
                    category.StartsWith("MiniDataProfiler", StringComparison.Ordinal);
            });

            // TimeProvider
            services.AddSingleton<TimeProvider>(new StaticTimeProvider { DateTime = DateTimeOffset.Now });

            // Data
            RemoveService(services, typeof(IDbProvider));
            // TODO
        });

        base.ConfigureWebHost(builder);
    }

    protected override void ConfigureClient(HttpClient client)
    {
        // TODO
    }

    private static void RemoveService(IServiceCollection services, Type type)
    {
        var descriptor = services.FirstOrDefault(d => d.ServiceType == type);
        if (descriptor is not null)
        {
            services.Remove(descriptor);
        }
    }

    //--------------------------------------------------------------------------------
    // Helper
    //--------------------------------------------------------------------------------

    public void SetProcessDateTime(DateTime dateTime)
    {
        var timeProvider = (StaticTimeProvider)Services.GetRequiredService<TimeProvider>();
        timeProvider.DateTime = dateTime;
    }
}
