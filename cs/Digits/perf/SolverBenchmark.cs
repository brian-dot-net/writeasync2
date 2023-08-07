// Copyright (c) Brian Rogers. All rights reserved.

using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Digits.Perf;

[MemoryDiagnoser]
public class SolverBenchmark
{
    private readonly Solver _solver;

    public SolverBenchmark()
    {
        _solver = new Solver(415, new Board(Enumerable.Range(1, 6)));
    }

    [Benchmark]
    public int Solve()
    {
        int n = 0;
        _solver.Solve(ms => n += ms.Count);
        return n;
    }
}
