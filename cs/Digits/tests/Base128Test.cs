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
    [InlineData(0)]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(127)]
    public void ReadOneByte(byte value)
    {
        Span<byte> input = stackalloc byte[1];
        input[0] = value;

        Base128.Read(input).Should().Be(value);
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
    [InlineData(384, 128)]
    [InlineData(1000, 488)]
    [InlineData(10200, 5080)]
    [InlineData(16383, 8191)]
    public void ReadTwoBytes(ushort value, short expected)
    {
        Span<byte> input = stackalloc byte[2];
        input[0] = (byte)(value % 256);
        input[1] = (byte)(value / 256);

        Base128.Read(input).Should().Be(expected);
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
    [InlineData(98432, 16384)]
    [InlineData(100000, 17184)]
    [InlineData(1032896, 254272)]
    [InlineData(5805696, 1444608)]
    [InlineData(8388607, 2097151)]
    public void ReadThreeBytes(int value, int expected)
    {
        Span<byte> input = stackalloc byte[3];
        input[0] = (byte)(value % 256);
        input[1] = (byte)(value / 256 % 256);
        input[2] = (byte)(value / 65536 % 256);

        Base128.Read(input).Should().Be(expected);
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

    [Theory]
    [InlineData(25198720, 2097152)]
    [InlineData(500032896, 62091904)]
    [InlineData(1000000128, 124167424)]
    [InlineData(2008388736, 250432000)]
    [InlineData(2147483647, 268435455)]
    public void ReadFourBytes(int value, int expected)
    {
        Span<byte> input = stackalloc byte[4];
        input[0] = (byte)(value % 256);
        input[1] = (byte)(value / 256 % 256);
        input[2] = (byte)(value / 65536 % 256);
        input[3] = (byte)(value / 16777216 % 256);

        Base128.Read(input).Should().Be(expected);
    }

    [Theory]
    [InlineData(268435456, 128, 128, 128, 128, 1)]
    [InlineData(536870912, 128, 128, 128, 128, 2)]
    [InlineData(555555555, 227, 181, 244, 136, 2)]
    [InlineData(2147483647, 255, 255, 255, 255, 7)]
    public void WriteFiveBytes(int input, byte expected0, byte expected1, byte expected2, byte expected3, byte expected4)
    {
        Span<byte> output = stackalloc byte[5];

        Base128.Write(input, output).Should().Be(5);

        output[0].Should().Be(expected0);
        output[1].Should().Be(expected1);
        output[2].Should().Be(expected2);
        output[3].Should().Be(expected3);
        output[4].Should().Be(expected4);
    }
}
