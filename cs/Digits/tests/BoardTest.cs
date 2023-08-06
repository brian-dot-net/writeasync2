// Copyright (c) Brian Rogers. All rights reserved.

using FluentAssertions;
using Xunit;

namespace Digits.Tests;

public sealed class BoardTest
{
    [Fact]
    public void ToStringShowsNumbers()
    {
        var board = new Board(new[] { 1, 2, 3, 4, 5, 6 });

        board.ToString().Should().Be("1,2,3,4,5,6");
    }

    [Theory]
    [InlineData(0, 1, '+', "3,5,10,20,50")]
    [InlineData(1, 2, '-', "1,3,10,20,50")]
    [InlineData(3, 4, '*', "1,2,5,50,200")]
    [InlineData(1, 5, '/', "1,5,10,20,25")]
    public void MoveValid(int n1, int n2, char op, string result)
    {
        Board board1 = new(new[] { 1, 2, 5, 10, 20, 50 });

        Board board2 = board1.TryMove(n1, n2, op);

        board2.ToString().Should().Be(result);
        board2.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(0, 0, '+', "")]
    [InlineData(1, 10, '-', "")]
    [InlineData(6, 1, '*', "")]
    [InlineData(-1, 2, '/', "")]
    [InlineData(3, -1, '+', "")]
    [InlineData(4, 5, '/', "")]
    [InlineData(0, 3, '/', "")]
    public void MoveInvalid(int n1, int n2, char op, string result)
    {
        Board board1 = new(new[] { 0, 2, 4, 8, 16, 25 });

        Board board2 = board1.TryMove(n1, n2, op);

        board2.ToString().Should().Be(result);
        board2.IsValid.Should().BeFalse();
    }
}
