// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Digits.Perf;

public class BoardBenchmark
{
    private int[] _n;

    public BoardBenchmark()
    {
        _n = Array.Empty<int>();
    }

    [Params(2, 6)]
    public int N { get; set; }

    [GlobalSetup]
    public void GlobalSetup() => _n = Enumerable.Range(1, N).ToArray();

    [Benchmark]
    public int Str() => new Board(_n).ToString().Length;
}
