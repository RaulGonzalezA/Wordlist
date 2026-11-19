namespace Wordlist.Application;

public sealed class CompoundWordFinder
{
    private const int TargetLength = 6;

    public static IEnumerable<CompoundWord> Find(IEnumerable<string> words)
    {
        var dictionary = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var candidates = new List<string>();

        foreach (var word in words)
        {
            if (dictionary.Add(word) && word.Length == TargetLength && word.All(char.IsLetter))
            {
                candidates.Add(word);
            }
        }

        foreach (var word in candidates)
        {
            for (var split = 1; split < word.Length; split++)
            {
                var left = word[..split];
                var right = word[split..];

                if (dictionary.Contains(left) && dictionary.Contains(right))
                {
                    yield return new CompoundWord(left, right, word);
                    break;
                }
            }
        }
    }
}
