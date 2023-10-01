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

    [Fact]
    public void ToStringShowsMove()
    {
        var board = new Board(new[] { 1, 2, 3, 4, 5, 6 });

        board.ToString(new Move(2, 4, '*')).Should().Be("5 * 3 = 15");
    }

    [Theory]
    [InlineData(0, 1, Ops.Add, "3,5,10,20,50")]
    [InlineData(1, 2, Ops.Subtract, "1,3,10,20,50")]
    [InlineData(3, 4, Ops.Multiply, "1,2,5,50,200")]
    [InlineData(1, 5, Ops.Divide, "1,5,10,20,25")]
    public void MoveValid(byte i1, byte i2, char op, string result)
    {
        Board board1 = new(new[] { 1, 2, 5, 10, 20, 50 });

        Board board2 = board1.TryMove(new Move(i1, i2, op), -1, out _);

        board2.ToString().Should().Be(result);
        board2.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(0, 0, Ops.Add, "")]
    [InlineData(1, 10, Ops.Subtract, "")]
    [InlineData(6, 1, Ops.Multiply, "")]
    [InlineData(4, 5, Ops.Divide, "")]
    [InlineData(0, 3, Ops.Divide, "")]
    [InlineData(1, 2, 'X', "")]
    public void MoveInvalid(byte i1, byte i2, char op, string result)
    {
        Board board1 = new(new[] { 0, 2, 4, 8, 16, 25 });

        Board board2 = board1.TryMove(new Move(i1, i2, op), -1, out _);

        board2.ToString().Should().Be(result);
        board2.IsValid.Should().BeFalse();
    }
}
