namespace Example.Endpoints;

public class DataEndpointTests(ApiApplicationFactory factory, ITestOutputHelper outputHelper)
    : ApiApplicationTest<DataEndpointTests>(factory, outputHelper)
{
    [Fact]
    public async Task GetList_ReturnsOk()
    {
        // Arrange
        using var client = Factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/data/", TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
