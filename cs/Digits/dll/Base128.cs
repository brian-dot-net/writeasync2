// Copyright (c) Brian Rogers. All rights reserved.

using System;

namespace Digits;

public static class Base128
{
    public static int Write(int value, Span<byte> output)
    {
        output[0] = (byte)value;
        return 1;
    }
}
