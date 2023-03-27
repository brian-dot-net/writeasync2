// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Threading;
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

    [Fact]
    public void Timestamp()
    {
        ITimeProvider time = Init();

        long start = time.GetTimestamp();

        Wait(time, TimeSpan.FromMilliseconds(123));

        long end = time.GetTimestamp();

        TimeSpan elapsed = time.GetElapsedTime(start, end);

        elapsed.Should().BeCloseTo(TimeSpan.FromSeconds(0.123), TimeSpan.FromMilliseconds(50));
    }

    [Fact]
    public void TimerImmediateNonPeriodic()
    {
        var evt = new CountdownEvent(1);
        ITimeProvider time = Init();

        using ITimer timer = time.CreateTimer(o => evt.Signal(int.Parse(o?.ToString() ?? "-1")), "1", TimeSpan.Zero, TimeSpan.Zero);

        evt.Wait(TimeSpan.FromMilliseconds(100)).Should().BeTrue();
    }

    [Fact]
    public void TimerDelayedNonPeriodic()
    {
        var evt = new CountdownEvent(1);
        ITimeProvider time = Init();

        using ITimer timer = time.CreateTimer(o => evt.Signal(int.Parse(o?.ToString() ?? "-1")), "1", TimeSpan.FromMilliseconds(50), TimeSpan.Zero);

        evt.Wait(TimeSpan.Zero).Should().BeFalse();

        Wait(time, TimeSpan.FromMilliseconds(100));

        evt.Wait(TimeSpan.Zero).Should().BeTrue();
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
