// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Digits;

public sealed class Board
{
    private static readonly Board Invalid = new(Enumerable.Empty<int>());

    private readonly int[] _numbers;

    public Board(IEnumerable<int> numbers)
    {
        _numbers = numbers.ToArray();
    }

    public Board Move(int n1, int n2, char op)
    {
        if (n1 == n2)
        {
            return Invalid;
        }

        int result = op switch
        {
            '+' => _numbers[n2] + _numbers[n1],
            '-' => _numbers[n2] - _numbers[n1],
            '*' => _numbers[n2] * _numbers[n1],
            '/' => _numbers[n2] / _numbers[n1],
            _ => throw new NotImplementedException()
        };

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
}
