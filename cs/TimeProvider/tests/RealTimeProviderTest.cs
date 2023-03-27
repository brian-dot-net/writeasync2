// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Threading;

namespace TimeProvider.Tests;

public sealed class RealTimeProviderTest : TimeProviderTest
{
    protected override ITimeProvider Init() => new RealTimeProvider();

    protected override void Wait(ITimeProvider provider, TimeSpan duration) => Thread.Sleep(duration);
}
