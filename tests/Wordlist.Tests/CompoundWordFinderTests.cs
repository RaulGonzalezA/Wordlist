using FluentAssertions;
using Wordlist.Application;
using Xunit;

namespace Wordlist.Tests;

public sealed class CompoundWordFinderTests
{
    [Fact]
    public void Finds_a_six_letter_word_by_combining_two_words_from_the_list()
    {
        var matches = CompoundWordFinder.Find(["al", "bums", "albums"]);

        var match = matches.Should().ContainSingle().Which;
        match.Left.Should().Be("al");
        match.Right.Should().Be("bums");
        match.Word.Should().Be("albums");
    }

    [Theory]
    [InlineData("a", "bcdef")]
    [InlineData("ab", "cdef")]
    [InlineData("abc", "def")]
    [InlineData("abcd", "ef")]
    [InlineData("abcde", "f")]
    public void Finds_a_match_at_each_possible_split_position(string left, string right)
    {
        var word = left + right;

        var matches = CompoundWordFinder.Find([word, left, right]);

        matches.Should().Equal(new CompoundWord(left, right, word));
    }

    [Theory]
    [InlineData("at", "one")]
    [InlineData("re", "tests")]
    public void Omits_a_candidate_when_its_length_is_not_six(string left, string right)
    {
        var matches = CompoundWordFinder.Find([left, right, left + right]);

        matches.Should().BeEmpty();
    }

    [Fact]
    public void Omits_a_six_character_candidate_that_contains_a_hyphen()
    {
        var matches = CompoundWordFinder.Find(["ab", "-cde", "ab-cde"]);

        matches.Should().BeEmpty();
    }

    [Theory]
    [InlineData("al")]
    [InlineData("bums")]
    public void Omits_a_candidate_when_one_of_its_parts_is_missing(string availablePart)
    {
        var matches = CompoundWordFinder.Find([availablePart, "albums"]);

        matches.Should().BeEmpty();
    }

    [Fact]
    public void Omits_a_combination_when_the_resulting_word_is_missing()
    {
        var matches = CompoundWordFinder.Find(["al", "bums"]);

        matches.Should().BeEmpty();
    }

    [Fact]
    public void Matches_without_case_sensitivity_and_preserves_the_target_spelling()
    {
        var matches = CompoundWordFinder.Find(["AL", "bUMS", "Albums"]);

        matches.Should().Equal(new CompoundWord("Al", "bums", "Albums"));
    }

    [Fact]
    public void Returns_the_first_valid_split_once_per_target_in_input_order()
    {
        var matches = CompoundWordFinder.Find(
            ["foobar", "FOOBAR", "foobar", "hereby", "fo", "obar", "foo", "bar", "here", "by"]);

        matches.Should().Equal(
            new CompoundWord("fo", "obar", "foobar"),
            new CompoundWord("here", "by", "hereby"));
    }

    [Fact]
    public void Allows_both_parts_to_match_the_same_dictionary_entry()
    {
        var matches = CompoundWordFinder.Find(["can", "cancan"]);

        matches.Should().Equal(new CompoundWord("can", "can", "cancan"));
    }
}
