// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Diagnostics;
using System.Threading;

namespace TimeProvider;

public sealed class RealTimeProvider : ITimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;

    public TimeZoneInfo LocalTimeZone => TimeZoneInfo.Local;

    public long TimestampFrequency => Stopwatch.Frequency;

    public long GetTimestamp() => Stopwatch.GetTimestamp();

    public ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period) => new TimerWrapper(callback, state, dueTime, period);

    private sealed class TimerWrapper : ITimer
    {
        private readonly Timer _timer;

        public TimerWrapper(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period)
        {
            _timer = new Timer(callback, state, dueTime, period);
        }

        public void Dispose() => _timer.Dispose();
    }
}
