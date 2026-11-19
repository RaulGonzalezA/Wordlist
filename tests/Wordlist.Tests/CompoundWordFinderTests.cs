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

        var matches = CompoundWordFinder.Find(["al", "bums", "albums"]);

        var match = matches.Should().ContainSingle().Which;
        match.Left.Should().Be("al");
        match.Right.Should().Be("bums");
        match.Word.Should().Be("albums");
    }

    [Fact]
    public void Omits_a_six_character_candidate_that_contains_a_hyphen()
    {
        var matches = CompoundWordFinder.Find(["ab", "-cde", "ab-cde"]);

        matches.Should().BeEmpty();
    }

    [Fact]
    public void Omits_a_candidate_when_one_of_its_parts_is_missing()
    {
        var matches = CompoundWordFinder.Find(["al", "albums"]);

        matches.Should().BeEmpty();
    }

    [Fact]
    public void Omits_a_combination_when_the_resulting_word_is_missing()
    {
        var matches = CompoundWordFinder.Find(["al", "bums"]);

        matches.Should().BeEmpty();
    }
}
