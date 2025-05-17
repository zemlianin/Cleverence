using System.Runtime.CompilerServices;
using System.Text;

namespace Task1
{
    internal class FastArchiver : IArchiver
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Compress(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var length = input.Length;
            var result = new StringBuilder(capacity: length);

            var count = 1;
            var current = input[0];

            for (var i = 1; i < length; ++i)
            {
                if (input[i] == current)
                {
                    count++;
                }
                else
                {
                    result.Append(current);

                    if (count > 1)
                    {
                        result.Append(count);
                    }

                    current = input[i];
                    count = 1;
                }
            }

            result.Append(current);

            if (count > 1)
            {  
                result.Append(count);
            }

            return result.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Decompress(string compressed)
        {
            if (string.IsNullOrEmpty(compressed))
            {
                return string.Empty;
            }

            var estimatedSize = compressed.Length * 4; // предположение
            var result = new StringBuilder(capacity: estimatedSize);
            var i = 0;

            while (i < compressed.Length)
            {
                var symbol = compressed[i++];
                var count = 0;

                // Быстрое чтение
                while (i < compressed.Length && (uint)(compressed[i] - '0') <= 9)
                {
                    count = count * 10 + (compressed[i++] - '0');
                }

                if (count == 0)
                    count = 1;

                result.Append(symbol, count);
            }

            return result.ToString();
        }
    }
}
