// Copyright (c) Brian Rogers. All rights reserved.

using FluentAssertions;
using Xunit;

namespace Digits.Tests;

public sealed class HelloTest
{
    [Fact]
    public void ToStringSaysHello()
    {
        var h = new Hello("world");

        h.ToString().Should().Be("Hello, world!");
    }
}
