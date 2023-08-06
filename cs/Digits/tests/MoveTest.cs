// Copyright (c) Brian Rogers. All rights reserved.

using FluentAssertions;
using Xunit;

namespace Digits.Tests;

public sealed class MoveTest
{
    [Fact]
    public void ToStringShowsOps()
    {
        var move = new Move(1, 2, '+');

        move.ToString().Should().Be("[2]+[1]");
    }
}
