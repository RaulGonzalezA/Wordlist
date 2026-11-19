namespace Wordlist.Infrastructure;

public static class WordlistReader
{
    public static IEnumerable<string> Read(TextReader input)
    {
        while (input.ReadLine() is { } line)
        {
            var word = line.Trim();
            if (word.Length > 0)
            {
                yield return word;
            }
        }
    }
}
