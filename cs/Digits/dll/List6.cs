// Copyright (c) Brian Rogers. All rights reserved.

namespace Digits;

public readonly struct List6
{
    private readonly ulong _value;

    private List6(int value)
    {
        _value = (ulong)value;
    }

    public int this[int index] => (int)_value;

    public byte Count => 1;

    public List6 Add(int value) => new(value);

}
