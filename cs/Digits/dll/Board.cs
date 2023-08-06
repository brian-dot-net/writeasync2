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

    public Board Move(int n1, int n2, char op)
    {
        if (n1 == n2 || (n1 < 0) || (n1 >= _numbers.Length) || (n2 < 0) || (n2 >= _numbers.Length))
        {
            return Invalid;
        }

        int result = Calculate(n1, n2, op);
        if (result == InvalidNumber)
        {
            return Invalid;
        }

        var numbers = new List<int> { result };
        for (int i = 0; i < _numbers.Length; i++)
        {
            if (i != n1 && i != n2)
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
            '+' => _numbers[n2] + _numbers[n1],
            '-' => _numbers[n2] - _numbers[n1],
            '*' => _numbers[n2] * _numbers[n1],
            '/' => Divide(n1, n2),
            _ => throw new NotImplementedException()
        };
    }

    private int Divide(int n1, int n2)
    {
        int d1 = _numbers[n2];
        int d2 = _numbers[n1];
        if (d2 == 0)
        {
            return InvalidNumber;
        }

        (int div, int rem) = Math.DivRem(d1, d2);
        return rem == 0 ? div : InvalidNumber;
    }
}
