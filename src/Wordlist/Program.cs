using Wordlist.Application;
using Wordlist.Infrastructure;
using Wordlist.Presentation;

const string fileName = "wordlist.txt";

try
{
    using var input = OpenWordlist(fileName);
    var words = WordlistReader.Read(input);
    var matches = CompoundWordFinder.Find(words);
    CompoundWordWriter.Write(matches, Console.Out);
    return 0;
}
catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
{
    Console.Error.WriteLine($"Could not process {fileName}: {exception.Message}");
    return 1;
}

static StreamReader OpenWordlist(string fileName)
{
    try
    {
        return File.OpenText(fileName);
    }
    catch (FileNotFoundException)
    {
        return File.OpenText(Path.Combine(AppContext.BaseDirectory, fileName));
    }
}
