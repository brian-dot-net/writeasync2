// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

    [Fact]
    public void TimerDelayedPeriodic()
    {
        var evt = new CountdownEvent(10);
        ITimeProvider time = Init();

        // |......F..|.F....F..|.F...
        // |         |         |
        // 0 ms      100 ms    200 ms
        //
        // F = timer fires
        using (ITimer timer = time.CreateTimer(o => evt.Signal(int.Parse(o?.ToString() ?? "-1")), "1", TimeSpan.FromMilliseconds(70), TimeSpan.FromMilliseconds(50)))
        {
            evt.CurrentCount.Should().Be(10);

            Wait(time, TimeSpan.FromMilliseconds(50));

            evt.CurrentCount.Should().Be(10);

            Wait(time, TimeSpan.FromMilliseconds(50));

            evt.CurrentCount.Should().BeInRange(8, 9);

            Wait(time, TimeSpan.FromMilliseconds(50));

            evt.CurrentCount.Should().BeLessThanOrEqualTo(9);
        }

        Wait(time, TimeSpan.FromMilliseconds(50));

        evt.CurrentCount.Should().BeInRange(7, 8);
    }

    [Fact]
    public void TimerChangeFromPeriodicToDisabled()
    {
        var evt = new CountdownEvent(10);
        ITimeProvider time = Init();

        using ITimer timer = time.CreateTimer(o => evt.Signal(int.Parse(o?.ToString() ?? "-1")), "1", TimeSpan.Zero, TimeSpan.FromMilliseconds(40));

        Wait(time, TimeSpan.FromMilliseconds(50));

        evt.CurrentCount.Should().Be(8);

        timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan).Should().BeTrue();

        Wait(time, TimeSpan.FromMilliseconds(50));

        evt.CurrentCount.Should().BeInRange(7, 8);

        Wait(time, TimeSpan.FromMilliseconds(50));

        evt.CurrentCount.Should().BeInRange(7, 8);
    }

    [Fact]
    public void TimerChangeFromDisabledToPeriodic()
    {
        var evt = new CountdownEvent(10);
        ITimeProvider time = Init();

        using ITimer timer = time.CreateTimer(o => evt.Signal(int.Parse(o?.ToString() ?? "-1")), "1", Timeout.InfiniteTimeSpan, TimeSpan.Zero);

        Wait(time, TimeSpan.FromMilliseconds(50));

        evt.CurrentCount.Should().Be(10);

        timer.Change(TimeSpan.FromMilliseconds(20), TimeSpan.FromMilliseconds(50)).Should().BeTrue();

        Wait(time, TimeSpan.FromMilliseconds(50));

        evt.CurrentCount.Should().Be(9);

        Wait(time, TimeSpan.FromMilliseconds(50));

        evt.CurrentCount.Should().BeInRange(7, 8);
    }

    [Fact]
    public void TwoAlternatingTimers()
    {
        var evt = new CountdownEvent(20);
        ITimeProvider time = Init();

        // |....1.2..|....1.2..|....1.2..
        // |         |         |
        // 0 ms      100 ms    200 ms
        using ITimer timer1 = time.CreateTimer(o => evt.Signal(int.Parse(o?.ToString() ?? "-1")), "1", TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(100));
        using (ITimer timer2 = time.CreateTimer(o => evt.Signal(int.Parse(o?.ToString() ?? "-1")), "2", TimeSpan.FromMilliseconds(70), TimeSpan.FromMilliseconds(100)))
        {
            Wait(time, TimeSpan.FromMilliseconds(100));

            evt.CurrentCount.Should().Be(17);

            Wait(time, TimeSpan.FromMilliseconds(100));

            evt.CurrentCount.Should().Be(14);
        }

        Wait(time, TimeSpan.FromMilliseconds(100));

        evt.CurrentCount.Should().BeOneOf(11, 13);

        Wait(time, TimeSpan.FromMilliseconds(100));

        evt.CurrentCount.Should().BeOneOf(10, 12);
    }

    [Fact]
    public void TwoTimersFireManyDuringInterval()
    {
        var evt = new CountdownEvent(20);
        ITimeProvider time = Init();

        // |.1...1...1...1...1.|
        // | 2 2 2 2 2         |
        // |         |         |
        // 0 ms      100 ms    200 ms
        using ITimer timer1 = time.CreateTimer(o => evt.Signal(int.Parse(o?.ToString() ?? "-1")), "1", TimeSpan.FromMilliseconds(20), TimeSpan.FromMilliseconds(40));
        using (ITimer timer2 = time.CreateTimer(o => evt.Signal(int.Parse(o?.ToString() ?? "-1")), "2", TimeSpan.FromMilliseconds(20), TimeSpan.FromMilliseconds(20)))
        {
            Wait(time, TimeSpan.FromMilliseconds(100));

            evt.CurrentCount.Should().BeInRange(7, 10);
        }

        Wait(time, TimeSpan.FromMilliseconds(100));

        evt.CurrentCount.Should().BeInRange(3, 7);
    }

    [Fact]
    public void MultipleTimersDifferentOrder()
    {
        var times = new BlockingCollection<(string, DateTimeOffset)>();
        ITimeProvider time = Init();

        // |.3..2..1.|.3..1....|
        // |         |         |
        // 0 ms      100 ms    200 ms
        using (ITimer timer1 = time.CreateTimer(o => times.Add(((string?)o ?? "-1", time.UtcNow)), "1", TimeSpan.FromMilliseconds(80), TimeSpan.FromMilliseconds(70)))
        using (ITimer timer2 = time.CreateTimer(o => times.Add(((string?)o ?? "-1", time.UtcNow)), "2", TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(150)))
        using (ITimer timer3 = time.CreateTimer(o => times.Add(((string?)o ?? "-1", time.UtcNow)), "3", TimeSpan.FromMilliseconds(20), TimeSpan.FromMilliseconds(100)))
        {
            Wait(time, TimeSpan.FromMilliseconds(170));
        }

        times.CompleteAdding();

        times.Select(t => t.Item1).Should().HaveCount(5).And.ContainInOrder("3", "2", "1", "3", "1");
        times.Select(t => t.Item2).Should().BeInAscendingOrder();
    }

    [Fact]
    public void WaitUntilCanceled()
    {
        ITimeProvider time = Init();
        using var cts = new CancellationTokenSource();

        Task task = time.WaitAsync(Timeout.InfiniteTimeSpan, cts.Token);

        task.IsCompleted.Should().BeFalse();

        cts.Cancel();

        task.IsCanceled.Should().BeTrue();
    }

    [Fact]
    public void WaitUntilDuration()
    {
        ITimeProvider time = Init();

        Task task = time.WaitAsync(TimeSpan.FromMilliseconds(20));

        task.IsCompleted.Should().BeFalse();

        Wait(time, TimeSpan.FromMilliseconds(50));

        task.IsCompletedSuccessfully.Should().BeTrue();
    }

    [Fact]
    public void WaitRacesWithCanceled()
    {
        ITimeProvider time = Init();
        using var cts = new CancellationTokenSource();

        using ITimer timer = time.CreateTimer(o => ((CancellationTokenSource)o!).Cancel(), cts, TimeSpan.FromMilliseconds(70), TimeSpan.Zero);
        Task task = time.WaitAsync(TimeSpan.FromMilliseconds(70), cts.Token);

        task.IsCompleted.Should().BeFalse();

        Wait(time, TimeSpan.FromMilliseconds(100));

        task.IsFaulted.Should().BeFalse();
        task.IsCompleted.Should().BeTrue();
    }

    [Fact]
    public void AsyncDispose()
    {
        ITimeProvider time = Init();
        var evt = new CountdownEvent(20);

        ITimer timer = time.CreateTimer(o => ((CountdownEvent)o!).Signal(), evt, TimeSpan.FromMilliseconds(40), TimeSpan.FromMilliseconds(1));

        Wait(time, TimeSpan.FromMilliseconds(50));

        ValueTask task = timer.DisposeAsync();

        Wait(time, TimeSpan.FromMilliseconds(5));

        task.IsCompletedSuccessfully.Should().BeTrue();
        evt.CurrentCount.Should().BeGreaterThan(0);
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
