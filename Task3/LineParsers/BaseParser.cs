using System.Globalization;
using Task3.LineParsers;
using Task3.Models;

namespace Task3.FormatParser
{
    internal abstract class BaseParser : ILineParser
    {
        public bool TryParse(string line, out string formattedLine)
        {
            formattedLine = string.Empty;

            if (!TryParseInternal(line, out var parsed))
            {
                return false;
            }

            formattedLine = parsed.ToString();

            return true;
        }

        protected abstract bool TryParseInternal(string line, out LogLine parsed);

        protected bool TryNormalizeLevel(string rawLevel, out LogLevel logLevel)
        {
            if (LogLevelParser.TryParse(rawLevel, out var level))
            {
                logLevel = level;
                return true;
            }

            logLevel = LogLevel.UNDEF;

            return false;
        }

        protected bool TryNormalizeDate(string input, string inputFormat, out string normalizedDate)
        {
            try
            {
                var parsed = DateTime.ParseExact(input, inputFormat, CultureInfo.InvariantCulture);
                normalizedDate = parsed.ToString("dd-MM-yyyy");
                return true;
            }
            catch (FormatException)
            {
                normalizedDate = string.Empty;
                return false;
            }
        }

    }
}
