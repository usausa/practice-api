namespace Example.Endpoints;

public class DataEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public DataEndpointTests(WebApplicationFactory<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Fact]
    public async Task GetList_ReturnsOk()
    {
        var response = await client.GetAsync("/api/data/");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
