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
            if (word.Length > TargetLength || !dictionary.Add(word))
            {
                continue;
            }

            if (word.Length == TargetLength && word.All(char.IsLetter))
            {
                candidates.Add(word);
            }
        }

        foreach (var candidate in candidates)
        {
            for (var splitIndex = 1; splitIndex < candidate.Length; splitIndex++)
            {
                var left = candidate[..splitIndex];
                var right = candidate[splitIndex..];

                if (dictionary.Contains(left) && dictionary.Contains(right))
                {
                    yield return new CompoundWord(left, right, candidate);
                    break;
                }
            }
        }
    }
}
