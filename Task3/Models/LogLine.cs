namespace Task3.Models
{
    internal class LogLine
    {
        public string Date { get; set; } = string.Empty;     // dd-MM-yyyy
        public string Time { get; set; } = string.Empty;
        public LogLevel Level { get; set; } = LogLevel.UNDEF;
        public string Caller { get; set; } = "DEFAULT";
        public string Message { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Date}\t{Time}\t{Level}\t{Caller}\t{Message}";
        }
    }
}
