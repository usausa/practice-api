namespace Example.Web.Endpoints;

// ReSharper disable MemberCanBePrivate.Global
public static class DataEndpoint
{
    //--------------------------------------------------------------------------------
    // Endpoints
    //--------------------------------------------------------------------------------

    public static void MapDataEndpoints(this WebApplication app)
    {
        // TODO with names ?
        var group = app.MapGroup("/api/data");

        group.MapGet("/", HandleList);
        // TODO
    }

    //--------------------------------------------------------------------------------
    // List
    //--------------------------------------------------------------------------------

    [ExcludeFromCodeCoverage]
    [GenerateToString]
    public sealed partial class ListResponseEntry
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
    }

    [ExcludeFromCodeCoverage]
    [GenerateToString]
#pragma warning disable CA1819
    public sealed partial class ListResponse
    {
        public ListResponseEntry[] Entries { get; set; } = default!;
    }
#pragma warning restore CA1819

    private static async ValueTask<IResult> HandleList()
    {
        // TODO
        await Task.Delay(0);
        return Results.Ok(new ListResponse
        {
            Entries = Enumerable.Range(1, 10).Select(static x => new ListResponseEntry { Id = x, Name = $"Data-{x}" }).ToArray()
        });
    }
}
// ReSharper restore MemberCanBePrivate.Global
