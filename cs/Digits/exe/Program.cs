// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Digits;

internal sealed class Program
{
    private static readonly Stopwatch Time = Stopwatch.StartNew();

    public static void Main(string[] args)
    {
        try
        {
            var parsed = Args.Parse(args);
            var board = new Board(parsed.Numbers);
            var solver = new Solver(parsed.Target, board);
            Log($"Finding solutions for target={parsed.Target}, board=[{board}]...");
            solver.Solve(ms => PrintSolution(board, ms));
            Log("Done.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void Log(string message) => Console.WriteLine("[{0:000.000}] {1}", Time.Elapsed.TotalSeconds, message);

    private static void PrintSolution(Board board, IList<Move> moves)
    {
        foreach (Move move in moves)
        {
            Console.Write(board.ToString(move) + ". ");
            board = board.TryMove(move, 0, out _);
        }

        Console.WriteLine();
    }
}
