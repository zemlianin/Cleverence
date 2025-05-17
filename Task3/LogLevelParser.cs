using Task3.Models;

namespace Task3
{
    internal class LogLevelParser
    {
        public static bool TryParse(string raw, out LogLevel level)
        {
            switch (raw.ToUpper())
            {
                case "INFORMATION":
                case "INFO":
                    level = LogLevel.INFO;
                    return true;

                case "WARNING":
                case "WARN":
                    level = LogLevel.WARN;
                    return true;

                case "ERROR":
                    level = LogLevel.ERROR;
                    return true;

                case "DEBUG":
                    level = LogLevel.DEBUG;
                    return true;

                default:
                    level = LogLevel.UNDEF;
                    return false;
            }
        }
    }
}
