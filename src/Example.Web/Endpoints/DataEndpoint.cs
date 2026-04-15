namespace Example.Web.Endpoints;

using Example.Models;

// ReSharper disable MemberCanBePrivate.Global
public static partial class DataEndpoint
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
#pragma warning disable CA1819
    public sealed partial class ListResponse
    {
        public DataEntity[] Entries { get; set; } = default!;
    }
#pragma warning restore CA1819

    private static async ValueTask<IResult> HandleList()
    {
        // TODO
        await Task.Delay(0);
        return Results.Ok(new ListResponse
        {
            Entries = Enumerable.Range(1, 10).Select(static x => new DataEntity { Id = x, Name = $"Data-{x}", Flag = x % 2 == 0, DateTime = DateTime.Now }).ToArray()
        });
    }
}
// ReSharper restore MemberCanBePrivate.Global
