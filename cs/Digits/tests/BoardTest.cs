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
    public void MoveValid(int n1, int n2, char op, string result)
    {
        Board board1 = new(new[] { 1, 2, 5, 10, 20, 50 });

        Board board2 = board1.Move(n1, n2, op);

        board2.ToString().Should().Be(result);
    }
}
