// Copyright (c) Brian Rogers. All rights reserved.

using System;

namespace Digits;

internal sealed class Program
{
    public static void Main()
    {
        var b = new Board(new[] { 1, 2, 3, 4, 5, 6 });

        Console.WriteLine(b);
    }
}
