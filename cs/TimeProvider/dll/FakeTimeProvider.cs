// Copyright (c) Brian Rogers. All rights reserved.

using System;

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

    public void Wait(TimeSpan duration) => UtcNow += duration;
}
