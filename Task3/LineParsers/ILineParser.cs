namespace Task3.LineParsers
{
    internal interface ILineParser
    {
        public bool TryParse(string line, out string formattedLine);
    }
}
