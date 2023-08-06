// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Digits;

public sealed class Board
{
    private const int InvalidNumber = -1;

    private static readonly Board Invalid = new(Enumerable.Empty<int>());

    private readonly int[] _numbers;

    public Board(IEnumerable<int> numbers)
    {
        _numbers = numbers.ToArray();
    }

    public bool IsValid => _numbers.Length > 0;

    public Board TryMove(Move move)
    {
        if (!move.IsInRange(_numbers.Length))
        {
            return Invalid;
        }

        int result = Calculate(_numbers[move.I1], _numbers[move.I2], move.Op);
        if (result == InvalidNumber)
        {
            return Invalid;
        }

        var numbers = new List<int> { result };
        for (int i = 0; i < _numbers.Length; i++)
        {
            if (i != move.I1 && i != move.I2)
            {
                numbers.Add(_numbers[i]);
            }
        }

        numbers.Sort();

        return new Board(numbers);
    }

    public override string ToString() => string.Join(',', _numbers);

    private int Calculate(int n1, int n2, char op)
    {
        return op switch
        {
            '+' => n2 + n1,
            '-' => n2 - n1,
            '*' => n2 * n1,
            '/' => Divide(n2, n1),
            _ => InvalidNumber,
        };
    }

    private int Divide(int n, int d)
    {
        if (d == 0)
        {
            return InvalidNumber;
        }

        (int div, int rem) = Math.DivRem(n, d);
        return rem == 0 ? div : InvalidNumber;
    }
}
