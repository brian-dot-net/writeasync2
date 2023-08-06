// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Collections.Generic;

namespace Digits;

public sealed class Solver
{
    private readonly int _target;
    private readonly Board _board;

    public Solver(int target, Board board)
    {
        _target = target;
        _board = board;
    }

    public void Solve(Action<IList<Move>> found) => Solve(_board, found);

    private void Solve(Board current, Action<IList<Move>> found)
    {
        if (current.HasTarget(_target))
        {
            found(new List<Move>());
        }
    }
}
