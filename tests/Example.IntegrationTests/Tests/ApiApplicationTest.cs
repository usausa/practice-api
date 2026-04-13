namespace Example.Tests;

public abstract class ApiApplicationTest<T> : IClassFixture<ApiApplicationFactory>
    where T : ApiApplicationTest<T>
{
    protected ApiApplicationFactory Factory { get; }

    protected ApiApplicationTest(ApiApplicationFactory factory, ITestOutputHelper outputHelper)
    {
        factory.UseTestOutput(outputHelper);
        Factory = factory;

        // TODO database
        //var configuration = factory.Services.GetRequiredService<IConfiguration>();
        //var connectionString = configuration.GetConnectionString(setting.Connection) ??
        //                       configuration.GetConnectionString("Default")!;
        //factory.Services.GetRequiredService<SwitchableDbProvider>().SwitchConnection(connectionString);
    }
}
