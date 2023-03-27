// Copyright (c) Brian Rogers. All rights reserved.

using System;

namespace TimeProvider;

public interface ITimeProvider
{
    DateTimeOffset UtcNow { get; }

    TimeZoneInfo LocalTimeZone { get; }

    long GetTimestamp();

    long TimestampFrequency { get; }
}
