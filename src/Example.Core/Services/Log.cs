namespace Example.Services;

internal static partial class Log
{
    [LoggerMessage(Level = LogLevel.Warning, Message = "Insert duplicated. key=[{key}]")]
    public static partial void WarnInsertDuplicated(this ILogger logger, object key);
}
