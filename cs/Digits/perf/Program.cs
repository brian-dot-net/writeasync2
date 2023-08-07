// Copyright (c) Brian Rogers. All rights reserved.

using BenchmarkDotNet.Running;

namespace Digits.Perf;

internal sealed class Program
{
    public static void Main() => _ = BenchmarkRunner.Run<SolverBenchmark>();
}
