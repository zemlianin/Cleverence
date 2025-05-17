namespace Task1
{
    internal interface IArchiver
    {
        public string Compress(string input);
        public string Decompress(string compressed);
    }
}
