// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Threading;

namespace TimeProvider;

public interface ITimeProvider
{
    DateTimeOffset UtcNow { get; }

    TimeZoneInfo LocalTimeZone { get; }

    long GetTimestamp();

    long TimestampFrequency { get; }

    public ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period);
}
