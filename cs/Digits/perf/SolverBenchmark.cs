// Copyright (c) Brian Rogers. All rights reserved.

using BenchmarkDotNet.Attributes;

namespace Digits.Perf;

[MemoryDiagnoser]
public class SolverBenchmark
{
    private readonly Solver _solver;

    public SolverBenchmark()
    {
        _solver = new Solver(415, new Board(new short[] { 1, 2, 3, 4, 5, 6 }));
    }

    [Benchmark]
    public int Solve()
    {
        int n = 0;
        _solver.Solve(ms => n += ms.Count);
        return n;
    }
}
