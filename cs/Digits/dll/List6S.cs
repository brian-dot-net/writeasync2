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

    public short this[byte index]
    {
        get
        {
#pragma warning disable IDE0066 // Convert switch statement to expression
            switch (index)
            {
                case 0:
                    return _i0;
                case 1:
                    return _i1;
                case 2:
                    return _i2;
                case 3:
                    return _i3;
                case 4:
                    return _i4;
                default:
                    return _i5;
            };
#pragma warning restore IDE0066 // Convert switch statement to expression
        }
    }

    public byte Count => (byte)_len;

    public List6S Add(short value)
    {
#pragma warning disable IDE0066 // Convert switch statement to expression
        switch (Count)
        {
            case 0:
                return new(1, value);
            case 1:
                return new(2, _i0, value);
            case 2:
                return new(3, _i0, _i1, value);
            case 3:
                return new(4, _i0, _i1, _i2, value);
            case 4:
                return new(5, _i0, _i1, _i2, _i3, value);
            default:
                return new(6, _i0, _i1, _i2, _i3, _i4, value);
        };
#pragma warning restore IDE0066 // Convert switch statement to expression
    }
}
