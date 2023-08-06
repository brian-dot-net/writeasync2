// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

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

    public void Solve(Action<IList<Move>> found) => Solve(_board, Enumerable.Empty<Move>(), found);

    private void Solve(Board current, IEnumerable<Move> previous, Action<IList<Move>> found)
    {
        if (current.HasTarget(_target))
        {
            found(previous.ToList());
            return;
        }

        foreach (Move move in Move.Generate(current.Count))
        {
            Board nextBoard = current.TryMove(move);
            if (nextBoard.IsValid)
            {
                var next = previous.ToList();
                next.Add(move);
                Solve(nextBoard, next, found);
            }
        }
    }
}
