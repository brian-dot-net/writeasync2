// Copyright (c) Brian Rogers. All rights reserved.

namespace Digits;

public record Move(int I1, int I2, char Op)
{
    public bool IsInRange(int length)
    {
        return
            (I1 != I2) &&
            (I1 < length) &&
            (I2 < length) &&
            (I1 >= 0) &&
            (I2 >= 0);
    }

    public override string ToString() => $"[{I2}]{Op}[{I1}]";
}
