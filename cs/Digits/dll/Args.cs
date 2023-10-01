// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Digits;

public record Args(short Target, short[] Numbers)
{
    public static Args Parse(params string[] args)
    {
        if (args.Length < 2)
        {
            throw new ArgumentException("There must be at least two input values.", nameof(args));
        }

        var queue = new Queue<short>(args.Select(ParseShort));
        short target = queue.Dequeue();
        short[] numbers = queue.ToArray();
        return new Args(target, numbers);
    }

    private static short ParseShort(string value)
    {
        return short.TryParse(value, out short result)
            ? result switch
            {
                < 0 => throw new ArgumentOutOfRangeException(nameof(value), $"The value {result} was not greater than or equal to zero."),
                _ => result
            }
            : throw new FormatException($"The value '{value}' is not a valid short integer.");
    }
}
