using FluentAssertions;
using Wordlist.Infrastructure;
using Xunit;

namespace Wordlist.Tests;

public sealed class WordlistReaderTests
{
    [Fact]
    public void Trims_words_and_skips_blank_lines()
    {
        using var input = new StringReader(" al \n\n\tbums\n   \nalbums ");

        WordlistReader.Read(input).Should().Equal("al", "bums", "albums");
    }

    [Fact]
    public void Returns_no_words_for_empty_input()
    {
        using var input = new StringReader(string.Empty);

        WordlistReader.Read(input).Should().BeEmpty();
    }
}
