// Copyright (c) Brian Rogers. All rights reserved.

using System;

namespace TimeProvider.Extensions;

public static class TimeProviderExtensions
{
    private const long TicksPerSecond = 10000000;

    public static DateTimeOffset LocalNow(this ITimeProvider provider) => provider.UtcNow.ToOffset(provider.LocalTimeZone.BaseUtcOffset);

    public static TimeSpan GetElapsedTime(this ITimeProvider provider, long startingTimestamp, long endingTimestamp) => new(TicksPerSecond * (endingTimestamp - startingTimestamp) / provider.TimestampFrequency);
}
