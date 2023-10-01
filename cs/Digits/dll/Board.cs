// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Digits;

public readonly struct Board
{
    private const short InvalidNumber = -1;

    private static readonly Board Invalid = new(Enumerable.Empty<short>());

    private readonly short[] _numbers;

    public Board(IEnumerable<short> numbers)
        : this(numbers.ToArray())
    {
    }

    private Board(short[] numbers)
    {
        _numbers = numbers;
    }

    public bool IsValid => _numbers.Length > 1;

    public byte Count => (byte)_numbers.Length;

    public Board TryMove(Move move, short target, out bool solved)
    {
        solved = false;
        if (!move.IsInRange(Count))
        {
            return Invalid;
        }

        short result = Calculate(move);
        if (result == InvalidNumber)
        {
            return Invalid;
        }

        if (result == target)
        {
            solved = true;
            return Invalid;
        }

        int j = 0;
        short[] numbers = new short[Count - 1];
        numbers[j++] = result;
        for (int i = 0; i < Count; i++)
        {
            if (i != move.I1 && i != move.I2)
            {
                numbers[j++] = _numbers[i];
            }
        }

        Array.Sort(numbers);

        return new Board(numbers);
    }

    public override string ToString() => string.Join(',', _numbers);

    public string ToString(Move move) => $"{_numbers[move.I2]} {move.Op} {_numbers[move.I1]} = {Calculate(move)}";

    private short Calculate(Move move)
    {
        int n1 = _numbers[move.I1];
        int n2 = _numbers[move.I2];
        int result = move.Op switch
        {
            Ops.Add => n2 + n1,
            Ops.Subtract => n2 - n1,
            Ops.Multiply => n2 * n1,
            Ops.Divide => Divide(n2, n1),
            _ => int.MaxValue,
        };
        return (result > short.MaxValue) ? InvalidNumber : (short)result;
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
