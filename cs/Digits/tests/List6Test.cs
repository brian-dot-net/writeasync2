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
}
