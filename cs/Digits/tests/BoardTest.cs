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
}
