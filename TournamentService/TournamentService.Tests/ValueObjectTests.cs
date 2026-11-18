using TournamentService.ValueObjects;
using TournamentService.ValueObjects.Exceptions;

namespace TournamentService.Tests;

public sealed class ValueObjectTests
{
    [Fact]
    public void TournamentName_accepts_valid_value()
    {
        Assert.Equal("Autumn Cup", new TournamentName("Autumn Cup").Value);
    }

    [Fact]
    public void TournamentName_rejects_too_short_value()
    {
        Assert.ThrowsAny<Exception>(() => new TournamentName("A"));
    }

    [Theory]
    [InlineData((byte)1)]
    [InlineData((byte)101)]
    public void MaxTeams_rejects_values_outside_range(byte value)
    {
        Assert.ThrowsAny<Exception>(() => new MaxTeams(value));
    }

    [Theory]
    [InlineData((byte)2)]
    [InlineData((byte)100)]
    public void MaxTeams_accepts_boundary_values(byte value)
    {
        Assert.Equal(value, new MaxTeams(value).Value);
    }

    [Fact]
    public void Money_rejects_negative_value()
    {
        Assert.ThrowsAny<ArgumentException>(() => new Money(-1));
    }

    [Fact]
    public void Money_rounds_and_supports_operators()
    {
        var left = new Money(10.125m);
        var right = new Money(2.125m);

        Assert.Equal(10.13m, left.Value);
        Assert.Equal(12.26m, (left + right).Value);
        Assert.Equal(8.00m, (left - right).Value);
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.True(right < left);
        Assert.True(right <= left);
    }

    [Fact]
    public void RegisteredTeamsCount_increments_and_decrements()
    {
        var count = new RegisteredTeamsCount(0);

        Assert.Equal((byte)1, count.Increment().Value);
        Assert.Throws<InvalidOperationException>(() => new RegisteredTeamsCount(0).Decrement());
    }

    [Fact]
    public void RegisteredTeamsCount_rejects_overflow_increment()
    {
        Assert.Throws<InvalidOperationException>(
            () => new RegisteredTeamsCount(100).Increment());
    }

    [Fact]
    public void ValueObjects_support_equality()
    {
        var first = new UserName("player");
        var second = new UserName("player");
        var different = new UserName("admin");

        Assert.Equal(first, second);
        Assert.NotEqual(first, different);
        Assert.True(first == second);
        Assert.True(first != different);
        Assert.NotEqual(0, first.GetHashCode());
    }
}
