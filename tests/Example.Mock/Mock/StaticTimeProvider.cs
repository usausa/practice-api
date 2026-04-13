namespace Example.Mock;

public sealed class StaticTimeProvider : TimeProvider
{
    public DateTimeOffset DateTime { get; set; }

    public override DateTimeOffset GetUtcNow() => DateTime.ToUniversalTime();
}
