namespace Example.Tests;

public sealed class XunitLoggerProvider : ILoggerProvider
{
    private readonly ITestOutputHelper testOutputHelper;

    private readonly Func<string, LogLevel, bool> filter;

    public XunitLoggerProvider(ITestOutputHelper testOutputHelper, Func<string, LogLevel, bool> filter)
    {
        this.testOutputHelper = testOutputHelper;
        this.filter = filter;
    }

    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName) => new XunitLogger(testOutputHelper, categoryName, filter);
}

public sealed class XunitLogger : ILogger
{
    private readonly ITestOutputHelper testOutputHelper;

    private readonly string categoryName;

    private readonly Func<string, LogLevel, bool> filter;

    public XunitLogger(ITestOutputHelper testOutputHelper, string categoryName, Func<string, LogLevel, bool> filter)
    {
        this.testOutputHelper = testOutputHelper;
        this.categoryName = categoryName;
        this.filter = filter;
    }

    public IDisposable BeginScope<TState>(TState state)
        where TState : notnull => NopScope.Instance;

    public bool IsEnabled(LogLevel logLevel) => filter(categoryName, logLevel);

#pragma warning disable CA1031
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        try
        {
            testOutputHelper.WriteLine($"{DateTime.Now:HH:mm:ss.fff} [{LogLevelFormat(logLevel)}] [{categoryName}] [{Environment.CurrentManagedThreadId}] - {formatter(state, exception)}");
            if (exception?.StackTrace is not null)
            {
                testOutputHelper.WriteLine(exception.StackTrace);
            }
        }
        catch
        {
            // Ignore
        }
    }
#pragma warning restore CA1031

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string LogLevelFormat(LogLevel level)
    {
        return level switch
        {
            LogLevel.Trace => "TRACE",
            LogLevel.Debug => "DEBUG",
            LogLevel.Information => "INFO",
            LogLevel.Warning => "WARN",
            LogLevel.Error => "ERROR",
            LogLevel.Critical => "CRITICAL",
            _ => "NONE"
        };
    }

    private sealed class NopScope : IDisposable
    {
        public static readonly NopScope Instance = new();

        public void Dispose()
        {
        }
    }
}
