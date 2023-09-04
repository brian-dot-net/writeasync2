// Copyright (c) Brian Rogers. All rights reserved.

using System;
using FluentAssertions;
using Xunit;

namespace Digits.Tests;

public sealed class Base128Test
{
    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(127)]
    public void WriteOneByte(byte input)
    {
        Span<byte> output = stackalloc byte[1];

        Base128.Write(input, output).Should().Be(1);

        output[0].Should().Be(input);
    }

    [Theory]
    [InlineData(128, 128, 1)]
    [InlineData(256, 128, 2)]
    [InlineData(500, 244, 3)]
    [InlineData(1000, 232, 7)]
    [InlineData(10000, 144, 78)]
    [InlineData(16383, 255, 127)]
    public void WriteTwoBytes(int input, byte expected0, byte expected1)
    {
        Span<byte> output = stackalloc byte[2];

        Base128.Write(input, output).Should().Be(2);

        output[0].Should().Be(expected0);
        output[1].Should().Be(expected1);
    }
}
