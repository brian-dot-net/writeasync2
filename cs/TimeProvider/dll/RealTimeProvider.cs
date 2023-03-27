// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Diagnostics;

namespace TimeProvider;

public sealed class RealTimeProvider : ITimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;

    public TimeZoneInfo LocalTimeZone => TimeZoneInfo.Local;

    public long TimestampFrequency => Stopwatch.Frequency;

    public long GetTimestamp() => Stopwatch.GetTimestamp();
}
