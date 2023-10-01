// Copyright (c) Brian Rogers. All rights reserved.

using FluentAssertions;
using Xunit;

namespace Digits.Tests;

public sealed class List6STest
{
    [Fact]
    public void AddOne()
    {
        List6S list = default;

        list = list.Add(1);

        list.Count.Should().Be(1);
        list[0].Should().Be(1);
    }

    [Fact]
    public void AddTwo()
    {
        List6S list = default;

        list = list.Add(10);
        list = list.Add(20);

        list.Count.Should().Be(2);
        list[0].Should().Be(10);
        list[1].Should().Be(20);
    }

    [Fact]
    public void AddThree()
    {
        List6S list = default;

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
        List6S list = default;

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
        List6S list = default;

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

    [Fact]
    public void AddSix()
    {
        List6S list = default;

        list = list.Add(122);
        list = list.Add(123);
        list = list.Add(124);
        list = list.Add(125);
        list = list.Add(126);
        list = list.Add(127);

        list.Count.Should().Be(6);
        list[0].Should().Be(122);
        list[1].Should().Be(123);
        list[2].Should().Be(124);
        list[3].Should().Be(125);
        list[4].Should().Be(126);
        list[5].Should().Be(127);
    }

    [Fact]
    public void AddFiveOneLarger()
    {
        List6S list = default;

        list = list.Add(1);
        list = list.Add(2);
        list = list.Add(3);
        list = list.Add(4);
        list = list.Add(130);

        list.Count.Should().Be(5);
        list[0].Should().Be(1);
        list[1].Should().Be(2);
        list[2].Should().Be(3);
        list[3].Should().Be(4);
        list[4].Should().Be(130);
    }

    [Fact]
    public void AddFourTwoLarger()
    {
        List6S list = default;

        list = list.Add(1);
        list = list.Add(2);
        list = list.Add(130);
        list = list.Add(131);

        list.Count.Should().Be(4);
        list[0].Should().Be(1);
        list[1].Should().Be(2);
        list[2].Should().Be(130);
        list[3].Should().Be(131);
    }

    [Fact]
    public void AddThreeThreeLarger()
    {
        List6S list = default;

        list = list.Add(130);
        list = list.Add(131);
        list = list.Add(132);

        list.Count.Should().Be(3);
        list[0].Should().Be(130);
        list[1].Should().Be(131);
        list[2].Should().Be(132);
    }

    [Fact]
    public void AddTwoOneLargest()
    {
        List6S list = default;

        list = list.Add(1);
        list = list.Add(32767);

        list.Count.Should().Be(2);
        list[0].Should().Be(1);
        list[1].Should().Be(32767);
    }

    [Fact]
    public void AddThreeOneLargest()
    {
        List6S list = default;

        list = list.Add(1);
        list = list.Add(2);
        list = list.Add(32767);

        list.Count.Should().Be(3);
        list[0].Should().Be(1);
        list[1].Should().Be(2);
        list[2].Should().Be(32767);
    }
}
