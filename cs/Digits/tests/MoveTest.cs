// Copyright (c) Brian Rogers. All rights reserved.

using System.Linq;
using FluentAssertions;
using Xunit;

namespace Digits.Tests;

public sealed class MoveTest
{
    [Fact]
    public void ToStringShowsOps()
    {
        var move = new Move(1, 2, Ops.Add);

        move.ToString().Should().Be("[2]+[1]");
    }

    [Fact]
    public void Generate2()
    {
        var moves = Move.Generate(2).Select(m => m.ToString()).ToList();
        moves.Sort();

        moves.Should().HaveCount(4).And.ContainInOrder(
            "[1]-[0]",
            "[1]*[0]",
            "[1]/[0]",
            "[1]+[0]");
    }

    [Fact]
    public void Generate4()
    {
        var moves = Move.Generate(4).Select(m => m.ToString()).ToList();
        moves.Sort();

        moves.Should().HaveCount(24).And.ContainInOrder(
            "[1]-[0]",
            "[1]*[0]",
            "[1]/[0]",
            "[1]+[0]",
            "[2]-[0]",
            "[2]-[1]",
            "[2]*[0]",
            "[2]*[1]",
            "[2]/[0]",
            "[2]/[1]",
            "[2]+[0]",
            "[2]+[1]",
            "[3]-[0]",
            "[3]-[1]",
            "[3]-[2]",
            "[3]*[0]",
            "[3]*[1]",
            "[3]*[2]",
            "[3]/[0]",
            "[3]/[1]",
            "[3]/[2]",
            "[3]+[0]",
            "[3]+[1]",
            "[3]+[2]");
    }
}
