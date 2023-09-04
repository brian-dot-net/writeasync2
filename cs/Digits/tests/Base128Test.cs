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

    [Theory]
    [InlineData(16384, 128, 128, 1)]
    [InlineData(32768, 128, 128, 2)]
    [InlineData(50000, 208, 134, 3)]
    [InlineData(100000, 160, 141, 6)]
    [InlineData(1000000, 192, 132, 61)]
    [InlineData(2097151, 255, 255, 127)]
    public void WriteThreeBytes(int input, byte expected0, byte expected1, byte expected2)
    {
        Span<byte> output = stackalloc byte[3];

        Base128.Write(input, output).Should().Be(3);

        output[0].Should().Be(expected0);
        output[1].Should().Be(expected1);
        output[2].Should().Be(expected2);
    }

    [Theory]
    [InlineData(2097152, 128, 128, 128, 1)]
    [InlineData(4194304, 128, 128, 128, 2)]
    [InlineData(5000000, 192, 150, 177, 2)]
    [InlineData(10000000, 128, 173, 226, 4)]
    [InlineData(100000000, 128, 194, 215, 47)]
    [InlineData(268435455, 255, 255, 255, 127)]
    public void WriteFourBytes(int input, byte expected0, byte expected1, byte expected2, byte expected3)
    {
        Span<byte> output = stackalloc byte[4];

        Base128.Write(input, output).Should().Be(4);

        output[0].Should().Be(expected0);
        output[1].Should().Be(expected1);
        output[2].Should().Be(expected2);
        output[3].Should().Be(expected3);
    }
}
