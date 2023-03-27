// Copyright (c) Brian Rogers. All rights reserved.

using System;

namespace TimeProvider;

public sealed class RealTimeProvider : ITimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;

    public TimeZoneInfo LocalTimeZone => TimeZoneInfo.Local;
}
