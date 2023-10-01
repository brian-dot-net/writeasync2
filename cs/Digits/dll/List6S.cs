// Copyright (c) Brian Rogers. All rights reserved.

namespace Digits;

public readonly struct List6S
{
    private readonly short _len;
    private readonly short _i0;
    private readonly short _i1;
    private readonly short _i2;
    private readonly short _i3;
    private readonly short _i4;
    private readonly short _i5;

    private List6S(short len, short i0 = 0, short i1 = 0, short i2 = 0, short i3 = 0, short i4 = 0, short i5 = 0)
    {
        _len = len;
        _i0 = i0;
        _i1 = i1;
        _i2 = i2;
        _i3 = i3;
        _i4 = i4;
        _i5 = i5;
    }

    public short this[byte index] => index switch
    {
        0 => _i0,
        1 => _i1,
        2 => _i2,
        3 => _i3,
        4 => _i4,
        _ => _i5,
    };

    public byte Count => (byte)_len;

    public List6S Add(short value)
    {
        return Count switch
        {
            0 => new(1, value),
            1 => new(2, _i0, value),
            2 => new(3, _i0, _i1, value),
            3 => new(4, _i0, _i1, _i2, value),
            4 => new(5, _i0, _i1, _i2, _i3, value),
            5 => new(6, _i0, _i1, _i2, _i3, _i4, value),
            _ => new(),
        };
    }
}
