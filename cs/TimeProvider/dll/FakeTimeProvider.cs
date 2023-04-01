// Copyright (c) Brian Rogers. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading;

namespace TimeProvider;

public sealed class FakeTimeProvider : ITimeProvider
{
    private readonly LinkedList<FakeTimer> _timers;

    public FakeTimeProvider(DateTimeOffset utcNow, TimeZoneInfo localZone)
    {
        UtcNow = utcNow;
        LocalTimeZone = localZone;
        _timers = new LinkedList<FakeTimer>();
    }

    public DateTimeOffset UtcNow { get; private set; }

    public TimeZoneInfo LocalTimeZone { get; }

    public long TimestampFrequency => 1000000;

    public long GetTimestamp() => UtcNow.UtcTicks / 10;

    public ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period)
    {
        FakeTimer timer = InsertTimer(new(this, callback, state, dueTime, period));
        Wait(TimeSpan.Zero);
        return timer;
    }

    public void Wait(TimeSpan duration)
    {
        do
        {
            duration = WaitNext(duration);
        }
        while (duration > TimeSpan.Zero);
    }

    private TimeSpan WaitNext(TimeSpan duration)
    {
        DateTimeOffset nextTime = UtcNow + duration;
        FakeTimer? next = GetTimer(nextTime);
        if (next is null)
        {
            UtcNow = nextTime;
        }
        else if (!next.IsDisposed)
        {
            UtcNow = next.Deadline;
            next.Fire();
            _ = InsertTimer(next);
        }

        return nextTime - UtcNow;
    }

    private FakeTimer InsertTimer(FakeTimer timer)
    {
        LinkedListNode<FakeTimer>? node = _timers.First;
        while (node is not null)
        {
            if (node.Value.Deadline >= timer.Deadline)
            {
                return _timers.AddBefore(node, timer).Value;
            }

            node = node.Next;
        }

        return _timers.AddLast(timer).Value;
    }

    private FakeTimer? GetTimer(DateTimeOffset nextTime)
    {
        LinkedListNode<FakeTimer>? node = _timers.First;
        while (node is not null)
        {
            LinkedListNode<FakeTimer>? next = node.Next;
            FakeTimer timer = node.Value;
            if (node.Value.Deadline <= nextTime)
            {
                _timers.Remove(node);
                return timer;
            }

            node = next;
        }

        return null;
    }

    private sealed class FakeTimer : ITimer
    {
        private readonly ITimeProvider _parent;
        private readonly TimerCallback _callback;
        private readonly object? _state;

        public FakeTimer(ITimeProvider parent, TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period)
        {
            _parent = parent;
            _callback = callback;
            _state = state;
            _ = Change(dueTime, period);
        }

        public TimeSpan Period { get; private set; }

        public DateTimeOffset Deadline { get; private set; }

        public bool IsDisposed { get; private set; }

        public void Dispose() => IsDisposed = true;

        public bool Change(TimeSpan dueTime, TimeSpan period)
        {
            Period = period;
            Deadline = dueTime >= TimeSpan.Zero ? _parent.UtcNow + dueTime : DateTimeOffset.MaxValue;
            return true;
        }

        public void Fire()
        {
            _callback(_state);
            if (Period > TimeSpan.Zero)
            {
                Deadline += Period;
            }
            else
            {
                Deadline = DateTimeOffset.MaxValue;
            }
        }
    }
}
