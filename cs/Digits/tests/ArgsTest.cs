// Copyright (c) Brian Rogers. All rights reserved.

using System;
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

    [Fact]
    public void ParseFailsNoArgs()
    {
        Action act = () => Args.Parse();

        act.Should().Throw<ArgumentException>()
            .WithMessage("There must be at least two input values.*")
            .Which.ParamName.Should().Be("args");
    }

    [Fact]
    public void ParseFailsNotAnInt()
    {
        Action act = () => Args.Parse("2", "x");

        act.Should().Throw<FormatException>()
            .WithMessage("The value 'x' is not a valid integer.");
    }
}
