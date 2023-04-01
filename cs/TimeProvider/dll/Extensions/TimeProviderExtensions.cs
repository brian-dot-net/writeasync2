// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace TimeProvider.Extensions;

public static class TimeProviderExtensions
{
    private const long TicksPerSecond = 10000000;

    public static DateTimeOffset LocalNow(this ITimeProvider provider) => provider.UtcNow.ToOffset(provider.LocalTimeZone.BaseUtcOffset);

    public static TimeSpan GetElapsedTime(this ITimeProvider provider, long startingTimestamp, long endingTimestamp) => new(TicksPerSecond * (endingTimestamp - startingTimestamp) / provider.TimestampFrequency);

    public static async Task WaitAsync(this ITimeProvider provider, TimeSpan duration, CancellationToken token)
    {
        var tcs = new TaskCompletionSource();
        using ITimer timer = provider.CreateTimer(o => ((TaskCompletionSource)o!).SetResult(), tcs, duration, TimeSpan.Zero);
        using CancellationTokenRegistration reg = token.Register((o, t) => ((TaskCompletionSource)o!).SetCanceled(t), tcs);
        await tcs.Task;
    }
}
