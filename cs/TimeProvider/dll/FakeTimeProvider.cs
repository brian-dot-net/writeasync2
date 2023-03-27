// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Threading;

namespace TimeProvider;

public sealed class FakeTimeProvider : ITimeProvider
{
    private FakeTimer? _timer;
    private DateTimeOffset _deadline;

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
        _timer = new FakeTimer(callback, state, period);
        _deadline = UtcNow + dueTime;
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

        if (timer is not null && next >= _deadline)
        {
            next = _deadline;
            UtcNow = next;
            timer.Fire();
            _deadline += timer.Period;
            return next - _deadline;
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
        private readonly TimerCallback _callback;
        private readonly object? _state;

        public FakeTimer(TimerCallback callback, object? state, TimeSpan period)
        {
            _callback = callback;
            _state = state;
            Period = period;
        }

        public TimeSpan Period { get; }

        public bool IsDisposed { get; private set; }

        public void Dispose() => IsDisposed = true;

        public void Fire() => _callback(_state);
    }
}
