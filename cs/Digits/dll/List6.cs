// Copyright (c) Brian Rogers. All rights reserved.

using System;

namespace Digits;

public readonly struct List6
{
    private readonly ulong _raw;

    private List6(ulong raw)
    {
        _raw = raw;
    }

    public int this[byte index]
    {
        get
        {
            Span<byte> bytes = stackalloc byte[6];
            Unpack(bytes);
            return Base128.Read(bytes.Slice(index + Delta(index)));
        }
    }

    public byte Count => (byte)(_raw & 0xFF);

    private byte Delta(byte index)
    {
        byte bits = (byte)(_raw >> 56);
        return index switch
        {
            1 => (byte)(bits & 0x7),
            2 => (byte)((bits >> 3) & 0x3),
            3 => (byte)((bits >> 5) & 0x3),
            4 => (byte)((bits >> 7) & 0x1),
            _ => 0,
        };
    }

    public List6 Add(int value)
    {
        Span<byte> bytes = stackalloc byte[6];
        Unpack(bytes);
        int delta = Delta(Count);
        int len = Base128.Write(value, bytes.Slice(Count + delta));
        return new(Pack(bytes) | ((ulong)Count + 1) | Adjust(delta + len - 1));
    }

    private ulong Adjust(int delta)
    {
        byte bits = (byte)(_raw >> 56);
        bits |= Count switch
        {
            0 => (byte)delta,
            1 => (byte)(delta << 3),
            2 => (byte)(delta << 5),
            3 => (byte)(delta << 7),
            _ => 0,
        };
        return (ulong)bits << 56;
    }

    private static ulong Pack(ReadOnlySpan<byte> bytes)
    {
        ulong raw = bytes[0];
        raw |= (ulong)bytes[1] << 8;
        raw |= (ulong)bytes[2] << 16;
        raw |= (ulong)bytes[3] << 24;
        raw |= (ulong)bytes[4] << 32;
        raw |= (ulong)bytes[5] << 40;
        return raw << 8;
    }

    private void Unpack(Span<byte> bytes)
    {
        bytes[0] = (byte)(_raw >> 8);
        bytes[1] = (byte)(_raw >> 16);
        bytes[2] = (byte)(_raw >> 24);
        bytes[3] = (byte)(_raw >> 32);
        bytes[4] = (byte)(_raw >> 40);
        bytes[5] = (byte)(_raw >> 48);
    }
}
