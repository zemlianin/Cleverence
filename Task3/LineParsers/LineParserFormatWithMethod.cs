using System.Text.RegularExpressions;
using Task3.Models;

namespace Task3.FormatParser
{
    internal class LineParserFormatWithMethod : BaseParser
    {
        private static readonly Regex regex = new(@"^(?<date>\d{4}-\d{2}-\d{2}) (?<time>\d{2}:\d{2}:\d{2}\.\d+)\|\s*(?<level>\w+)\|.*?\|(?<method>[\w\.]+)\|\s*(?<message>.+)$");

        protected override bool TryParseInternal(string line, out LogLine parsed)
        {
            parsed = new LogLine();
            var match = regex.Match(line);

            if (!match.Success)
            {
                return false;
            }

            if (!TryNormalizeDate(match.Groups["date"].Value, "yyyy-MM-dd", out var normalizedDate))
            {
                return false;
            }

            parsed.Date = normalizedDate;
            parsed.Time = match.Groups["time"].Value;

            if (!TryNormalizeLevel(match.Groups["level"].Value, out var logLevel))
            {
                return false;
            }

            parsed.Level = logLevel;
            parsed.Caller = match.Groups["method"].Value;
            parsed.Message = match.Groups["message"].Value;

            return true;
        }

    }
}
