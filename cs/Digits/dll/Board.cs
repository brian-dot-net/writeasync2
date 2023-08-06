// Copyright (c) Brian Rogers. All rights reserved.

using System.Collections.Generic;
using System.Linq;

namespace Digits;

public sealed class Board
{
    private readonly int[] _numbers;

    public Board(IEnumerable<int> numbers)
    {
        _numbers = numbers.ToArray();
    }

    public override string ToString() => string.Join(',', _numbers);
}
