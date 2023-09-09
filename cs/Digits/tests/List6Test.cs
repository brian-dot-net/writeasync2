// Copyright (c) Brian Rogers. All rights reserved.

using FluentAssertions;
using Xunit;

namespace Digits.Tests;

public sealed class List6Test
{
    [Fact]
    public void AddOne()
    {
        List6 list = default;

        list = list.Add(1);

        list.Count.Should().Be(1);
        list[0].Should().Be(1);
    }

    [Fact]
    public void AddTwo()
    {
        List6 list = default;

        list = list.Add(10);
        list = list.Add(20);

        list.Count.Should().Be(2);
        list[0].Should().Be(10);
        list[1].Should().Be(20);
    }

    [Fact]
    public void AddThree()
    {
        List6 list = default;

        list = list.Add(16);
        list = list.Add(32);
        list = list.Add(64);

        list.Count.Should().Be(3);
        list[0].Should().Be(16);
        list[1].Should().Be(32);
        list[2].Should().Be(64);
    }

    [Fact]
    public void AddFour()
    {
        List6 list = default;

        list = list.Add(25);
        list = list.Add(50);
        list = list.Add(75);
        list = list.Add(100);

        list.Count.Should().Be(4);
        list[0].Should().Be(25);
        list[1].Should().Be(50);
        list[2].Should().Be(75);
        list[3].Should().Be(100);
    }

    [Fact]
    public void AddFive()
    {
        List6 list = default;

        list = list.Add(30);
        list = list.Add(50);
        list = list.Add(70);
        list = list.Add(90);
        list = list.Add(110);

        list.Count.Should().Be(5);
        list[0].Should().Be(30);
        list[1].Should().Be(50);
        list[2].Should().Be(70);
        list[3].Should().Be(90);
        list[4].Should().Be(110);
    }
}
