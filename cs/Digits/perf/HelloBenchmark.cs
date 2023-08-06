// Copyright (c) Brian Rogers. All rights reserved.

using BenchmarkDotNet.Attributes;

namespace Digits.Perf;

public class HelloBenchmark
{
    private string _s;

    public HelloBenchmark()
    {
        _s = string.Empty;
    }

    [Params(1, 100)]
    public int L { get; set; }

    [GlobalSetup]
    public void GlobalSetup() => _s = new string('z', L);

    [Benchmark]
    public int Str() => new Hello(_s).ToString().Length;
}
