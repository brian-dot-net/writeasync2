// Copyright (c) Brian Rogers. All rights reserved.

using System.Collections.Generic;

namespace Digits;

public record Move(int I1, int I2, char Op)
{
    public static IEnumerable<Move> Generate(int length)
    {
        for (int i1 = 0; i1 < length; ++i1)
        {
            for (int i2 = i1 + 1; i2 < length; ++i2)
            {
                foreach (char op in Ops.All)
                {
                    yield return new Move(i1, i2, op);
                }
            }
        }
    }

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
