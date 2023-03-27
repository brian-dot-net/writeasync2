// Copyright (c) Brian Rogers. All rights reserved.

using System;

namespace TimeProvider.Extensions;

public static class TimeProviderExtensions
{
    public static DateTimeOffset LocalNow(this ITimeProvider provider) => provider.UtcNow.ToOffset(provider.LocalTimeZone.BaseUtcOffset);
}
