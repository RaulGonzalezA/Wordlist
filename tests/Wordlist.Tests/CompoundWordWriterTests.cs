using FluentAssertions;
using Wordlist.Application;
using Wordlist.Presentation;
using Xunit;

namespace Wordlist.Tests;

public sealed class CompoundWordWriterTests
{
    [Fact]
    public void Writes_each_match_on_its_own_line_in_the_required_format()
    {
        using var output = new StringWriter();
        var matches = new[]
        {
            new CompoundWord("al", "bums", "albums"),
            new CompoundWord("bar", "ely", "barely")
        };

        CompoundWordWriter.Write(matches, output);

        output.ToString().Should().Be(
            $"al + bums => albums{output.NewLine}bar + ely => barely{output.NewLine}");
    }
}
