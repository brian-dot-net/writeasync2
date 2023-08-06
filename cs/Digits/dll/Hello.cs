// Copyright (c) Brian Rogers. All rights reserved.

namespace Digits;

public sealed class Hello
{
    private readonly string _name;

    public Hello(string name)
    {
        _name = name;
    }

    public override string ToString() => $"Hello, {_name}!";
}
