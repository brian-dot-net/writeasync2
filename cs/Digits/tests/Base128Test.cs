// Copyright (c) Brian Rogers. All rights reserved.

using System;
using FluentAssertions;
using Xunit;

namespace Digits.Tests;

public sealed class Base128Test
{
    [Fact]
    public void WriteOneByte()
    {
        Span<byte> output = stackalloc byte[1];

        Base128.Write(0, output).Should().Be(1);

        output[0].Should().Be(0);
    }
}
