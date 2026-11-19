using FluentAssertions;
using Wordlist.Application;
using Xunit;

namespace Wordlist.Tests;

public sealed class CompoundWordFinderTests
{
    [Fact]
    public void Finds_a_six_letter_word_by_combining_two_words_from_the_list()
    {
        var finder = new CompoundWordFinder();

        var matches = finder.Find(["al", "bums", "albums"]);

        var match = matches.Should().ContainSingle().Which;
        match.Left.Should().Be("al");
        match.Right.Should().Be("bums");
        match.Word.Should().Be("albums");
    }
}
