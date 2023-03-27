// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Threading;

namespace TimeProvider;

public sealed class FakeTimeProvider : ITimeProvider
{
    private FakeTimer? _timer;

    public FakeTimeProvider(DateTimeOffset utcNow, TimeZoneInfo localZone)
    {
        UtcNow = utcNow;
        LocalTimeZone = localZone;
    }

    public DateTimeOffset UtcNow { get; private set; }

    public TimeZoneInfo LocalTimeZone { get; }

    public long TimestampFrequency => 1000000;

    public long GetTimestamp() => UtcNow.UtcTicks / 10;

    public ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period)
    {
        _timer = new FakeTimer(this, callback, state, dueTime, period);
        Wait(TimeSpan.Zero);

        return _timer;
    }

    public void Wait(TimeSpan duration)
    {
        do
        {
            duration = WaitNext(duration);
        }
        while (duration > TimeSpan.Zero);
    }

    private TimeSpan WaitNext(TimeSpan duration)
    {
        DateTimeOffset next = UtcNow + duration;
        FakeTimer? timer = GetTimer();

        if (timer is not null && next >= timer.Deadline)
        {
            UtcNow = timer.Deadline;
            timer.Fire();
            return next - UtcNow;
        }

        UtcNow = next;
        return TimeSpan.Zero;
    }

    private FakeTimer? GetTimer()
    {
        if (_timer is not null && _timer.IsDisposed)
        {
            _timer = null;
        }

        return _timer;
    }

    private sealed class FakeTimer : ITimer
    {
        private readonly ITimeProvider _parent;
        private readonly TimerCallback _callback;
        private readonly object? _state;

        public FakeTimer(ITimeProvider parent, TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period)
        {
            _parent = parent;
            _callback = callback;
            _state = state;
            _ = Change(dueTime, period);
        }

        public TimeSpan Period { get; private set; }

        public DateTimeOffset Deadline { get; private set; }

        public bool IsDisposed { get; private set; }

        public void Dispose() => IsDisposed = true;

        public bool Change(TimeSpan dueTime, TimeSpan period)
        {
            Period = period;
            Deadline = _parent.UtcNow + dueTime;
            return true;
        }

        public void Fire()
        {
            _callback(_state);
            if (Period > TimeSpan.Zero)
            {
                Deadline += Period;
            }
            else
            {
                Deadline = DateTimeOffset.MaxValue;
            }
        }
    }
}
