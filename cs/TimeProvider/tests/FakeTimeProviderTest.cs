// Copyright (c) Brian Rogers. All rights reserved.

using System;
using FluentAssertions;

namespace TimeProvider.Tests;

public sealed class FakeTimeProviderTest : TimeProviderTest
{
    protected override ITimeProvider Init() => new FakeTimeProvider(DateTimeOffset.UtcNow, TimeZoneInfo.Local);

    protected override void Wait(ITimeProvider provider, TimeSpan duration)
    {
        FakeTimeProvider fake = provider.Should().BeOfType<FakeTimeProvider>().Subject;
        fake.Wait(duration);
    }
}
