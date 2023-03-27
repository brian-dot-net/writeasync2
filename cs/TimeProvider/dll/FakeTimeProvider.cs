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
        if (_deadline == UtcNow)
        {
            _timer.Fire();
        }

        return _timer;
    }

    public void Wait(TimeSpan duration)
    {
        UtcNow += duration;
        if (_timer is not null && UtcNow >= _deadline)
        {
            _timer.Fire();
        }
    }

    private sealed class FakeTimer : ITimer
    {
        private readonly TimerCallback _callback;
        private readonly object? _state;

        public FakeTimer(TimerCallback callback, object? state, TimeSpan period)
        {
            _callback = callback;
            _state = state;
            if (period != TimeSpan.Zero)
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
        }

        public void Fire() => _callback(_state);
    }
}
