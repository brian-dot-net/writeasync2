// Copyright (c) Brian Rogers. All rights reserved.

using System;

namespace Digits;

public static class Base128
{
    public static int Read(Span<byte> input) => input[0];

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
