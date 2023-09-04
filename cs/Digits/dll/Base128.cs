// Copyright (c) Brian Rogers. All rights reserved.

using System;

namespace Digits;

public static class Base128
{
    public static int Read(ReadOnlySpan<byte> input)
    {
        int i = -1;
        int output = 0;
        byte next;
        do
        {
            next = input[++i];
            output |= (next & 0x7F) << (7 * i);
        }
        while (next > 127);

        return output;
    }

    public static int Write(int value, Span<byte> output)
    {
        int i = 0;
        do
        {
            byte next = (byte)(value & 0x7F);
            if (value > 127)
            {
                next |= 0x80;
            }

            value >>= 7;
            output[i++] = next;
        }
        while (value > 0);

        return i;
    }
}
