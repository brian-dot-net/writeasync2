// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Digits;

public record Args(int Target, int[] Numbers)
{
    public static Args Parse(params string[] args)
    {
        if (args.Length < 2)
        {
            throw new ArgumentException("There must be at least two input values.", nameof(args));
        }

        var queue = new Queue<int>(args.Select(int.Parse));
        int target = queue.Dequeue();
        int[] numbers = queue.ToArray();
        return new Args(target, numbers);
    }
}
