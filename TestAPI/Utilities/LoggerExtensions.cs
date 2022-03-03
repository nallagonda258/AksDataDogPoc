using Serilog;
using Serilog.Formatting.Json;
using System.Runtime.CompilerServices;

namespace TestAPI.Utilities
{
    public static class LoggerExtensions
    {
        public static ILogger Enrich(this ILogger logger,
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return logger
                .ForContext("MemberName", memberName)
                .ForContext("LineNumber", sourceLineNumber);
        }

        public static ILogger Logger()
        {
            return new LoggerConfiguration().WriteTo.Console(new JsonFormatter()).CreateLogger();
        }
    }
}
