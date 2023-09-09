// Copyright (c) Brian Rogers. All rights reserved.

namespace Digits;

public readonly struct List6
{
    private readonly ulong _raw;

    private List6(ulong raw)
    {
        _raw = raw;
    }

    public int this[byte index] => (int)((_raw >> (8 * (index + 1))) & 0xFF);

    public byte Count => (byte)(_raw & 0xFF);

    public List6 Add(int value)
    {
        ulong raw = (ulong)value;
        raw <<= 8 * (Count + 1);
        raw |= _raw;
        return new(raw + 1);
    }
}
