// Copyright (c) Brian Rogers. All rights reserved.

using System.Collections.Generic;

namespace Digits;

public static class Ops
{
    public const char Add = '+';
    public const char Subtract = '-';
    public const char Multiply = '*';
    public const char Divide = '/';

    public static readonly IEnumerable<char> All = new[] { Add, Subtract, Multiply, Divide };
}
