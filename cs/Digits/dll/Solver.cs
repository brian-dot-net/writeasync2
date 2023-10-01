// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Digits;

public sealed class Solver
{
    private readonly short _target;
    private readonly Board _board;

    public Solver(short target, Board board)
    {
        _target = target;
        _board = board;
    }

    public void Solve(Action<IList<Move>> found) => Solve(_board, Enumerable.Empty<Move>(), found);

    private void Solve(Board current, IEnumerable<Move> previous, Action<IList<Move>> found)
    {
        foreach (Move move in Move.Generate(current.Count))
        {
            Board nextBoard = current.TryMove(move, _target, out bool solved);
            var next = previous.ToList();
            next.Add(move);
            if (solved)
            {
                found(next);
                return;
            }

            if (nextBoard.IsValid)
            {
                Solve(nextBoard, next, found);
            }
        }
    }
}
