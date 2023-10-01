// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Digits.Perf;

[MemoryDiagnoser]
public class BoardBenchmark
{
    private short[] _n;

    public BoardBenchmark()
    {
        _n = Array.Empty<short>();
    }

    [Params(2, 4, 6)]
    public int N { get; set; }

    [GlobalSetup]
    public void GlobalSetup() => _n = Enumerable.Range(1, N).Select(i => (short)i).ToArray();

    [Benchmark]
    public bool TryMove() => new Board(_n).TryMove(new Move(0, 1, Ops.Multiply), -1, out _).IsValid;
}
