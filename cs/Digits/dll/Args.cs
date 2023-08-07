// Copyright (c) Brian Rogers. All rights reserved.

using System.Collections.Generic;
using System.Linq;

namespace Digits;

public record Args(int Target, int[] Numbers)
{
    public static Args Parse(params string[] args)
    {
        var queue = new Queue<int>(args.Select(int.Parse));
        int target = queue.Dequeue();
        int[] numbers = queue.ToArray();
        return new Args(target, numbers);
    }
}
