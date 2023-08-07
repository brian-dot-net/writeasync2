// Copyright (c) Brian Rogers. All rights reserved.

using FluentAssertions;
using Xunit;

namespace Digits.Tests;

public sealed class ArgsTest
{
    [Fact]
    public void ParseTargetAndNumbers()
    {
        var args = Args.Parse("1", "2");

        args.Target.Should().Be(1);
        args.Numbers.Should().HaveCount(1).And.ContainInOrder(2);
    }

}
