using System.Text;

namespace Task1
{
    internal class Archiver : IArchiver
    {
        public string Compress(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var result = new StringBuilder();
            var count = 1;
            var currentSymbol = input[0];

            for (var i = 1; i < input.Length; i++)
            {
                if (input[i] == currentSymbol)
                {
                    count++;
                }
                else
                {
                    result.Append(currentSymbol);

                    if (count > 1)
                    {
                        result.Append(count);
                    }

                    currentSymbol = input[i];
                    count = 1;
                }
            }

            result.Append(currentSymbol);

            if (count > 1)
            {
                result.Append(count);
            }

            return result.ToString();
        }

        public string Decompress(string compressed)
        {
            if (string.IsNullOrEmpty(compressed))
            {
                return string.Empty;
            }

            var result = new StringBuilder();
            var i = 0;

            while (i < compressed.Length)
            {
                var symbol = compressed[i++];

                var amountOfSymbolString = new StringBuilder();

                while (i < compressed.Length && char.IsDigit(compressed[i]))
                {
                    amountOfSymbolString.Append(compressed[i]);
                    i++;
                }

                var count = amountOfSymbolString.Length != 0 ? int.Parse(amountOfSymbolString.ToString()) : 1;

                result.Append(new string(symbol, count));
            }

            return result.ToString();
        }
    }
}
