// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Threading;

namespace TimeProvider;

public sealed class FakeTimeProvider : ITimeProvider
{
    public FakeTimeProvider(DateTimeOffset utcNow, TimeZoneInfo localZone)
    {
        UtcNow = utcNow;
        LocalTimeZone = localZone;
    }

    public DateTimeOffset UtcNow { get; private set; }

    public TimeZoneInfo LocalTimeZone { get; }

    public long TimestampFrequency => 1000000;

    public long GetTimestamp() => UtcNow.UtcTicks / 10;

    public ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period) => new FakeTimer(callback, state, dueTime, period);

    public void Wait(TimeSpan duration) => UtcNow += duration;

    private sealed class FakeTimer : ITimer
    {
        private readonly TimerCallback _callback;
        private readonly object? _state;

        public FakeTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period)
        {
            _callback = callback;
            _state = state;
            if (dueTime != TimeSpan.Zero || period != TimeSpan.Zero)
            {
                throw new NotImplementedException();
            }

            Fire();
        }

        public void Dispose()
        {
        }

        private void Fire() => _callback(_state);
    }
}
