// Copyright (c) Brian Rogers. All rights reserved.

using System;
using FluentAssertions;
using TimeProvider.Extensions;
using Xunit;

namespace TimeProvider.Tests;

public abstract class TimeProviderTest
{
    [Fact]
    public void DateTime()
    {
        DateTimeOffset utcStart = DateTimeOffset.UtcNow;
        DateTimeOffset localStart = DateTimeOffset.Now;
        TimeZoneInfo zone = TimeZoneInfo.Local;
        ITimeProvider time = Init();

        TestTimeInfo(time, utcStart, localStart, zone);

        Wait(time, TimeSpan.FromMilliseconds(100));

        TestTimeInfo(time, utcStart + TimeSpan.FromSeconds(0.1d), localStart + TimeSpan.FromSeconds(0.1d), TimeZoneInfo.Local);
    }

    protected abstract ITimeProvider Init();

    protected abstract void Wait(ITimeProvider provider, TimeSpan duration);

    private static void TestTimeInfo(ITimeProvider time, DateTimeOffset utcExpected, DateTimeOffset localExpected, TimeZoneInfo zoneExpected)
    {
        DateTimeOffset utcNow = time.UtcNow;
        DateTimeOffset localNow = time.LocalNow();
        TimeZoneInfo localZone = time.LocalTimeZone;

        utcNow.Should().BeCloseTo(utcExpected, TimeSpan.FromMilliseconds(50));
        localNow.Should().BeCloseTo(localExpected, TimeSpan.FromMilliseconds(50));
        localZone.Should().Be(zoneExpected);
    }
}
