using Wordlist.Application;

namespace Wordlist.Presentation;

public static class CompoundWordWriter
{
    public static void Write(IEnumerable<CompoundWord> matches, TextWriter output)
    {
        foreach (var match in matches)
        {
            output.WriteLine($"{match.Left} + {match.Right} => {match.Word}");
        }
    }
}
