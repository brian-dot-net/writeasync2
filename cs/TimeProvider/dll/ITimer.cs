// Copyright (c) Brian Rogers. All rights reserved.

using System;

namespace TimeProvider;

public interface ITimer : IDisposable, IAsyncDisposable
{
    bool Change(TimeSpan dueTime, TimeSpan period);
}
