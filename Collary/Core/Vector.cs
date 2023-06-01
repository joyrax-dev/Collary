using System;
using System.Runtime.InteropServices;

namespace Collary.Core;


[StructLayout(LayoutKind.Sequential)]
public struct Vector2f : IEquatable<Vector2f>
{
    public Vector2f(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static Vector2f operator -(Vector2f v) => new Vector2f(-v.X, -v.Y);

    public static Vector2f operator -(Vector2f v1, Vector2f v2) => new Vector2f(v1.X - v2.X, v1.Y - v2.Y);

    public static Vector2f operator +(Vector2f v1, Vector2f v2) => new Vector2f(v1.X + v2.X, v1.Y + v2.Y);

    public static Vector2f operator *(Vector2f v, float x) => new Vector2f(v.X * x, v.Y * x);

    public static Vector2f operator *(float x, Vector2f v) => new Vector2f(v.X * x, v.Y * x);

    public static Vector2f operator /(Vector2f v, float x) => new Vector2f(v.X / x, v.Y / x);

    public static bool operator ==(Vector2f v1, Vector2f v2) => v1.Equals(v2);

    public static bool operator !=(Vector2f v1, Vector2f v2) => !v1.Equals(v2);

    public override string ToString() => $"[Vector2f] X({X}) Y({Y})";

    public override bool Equals(object obj) => ( obj is Vector2f ) && Equals((Vector2f)obj);

    public bool Equals(Vector2f other) => ( X == other.X ) && ( Y == other.Y );

    public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();

    public static explicit operator Vector2i(Vector2f v) => new Vector2i((int)v.X, (int)v.Y);

    public static explicit operator Vector2u(Vector2f v) => new Vector2u((uint)v.X, (uint)v.Y);

    public float X;

    public float Y;
}

[StructLayout(LayoutKind.Sequential)]
public struct Vector2i : IEquatable<Vector2i>
{
    public Vector2i(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Vector2i operator -(Vector2i v) => new Vector2i(-v.X, -v.Y);

    public static Vector2i operator -(Vector2i v1, Vector2i v2) => new Vector2i(v1.X - v2.X, v1.Y - v2.Y);

    public static Vector2i operator +(Vector2i v1, Vector2i v2) => new Vector2i(v1.X + v2.X, v1.Y + v2.Y);

    public static Vector2i operator *(Vector2i v, int x) => new Vector2i(v.X * x, v.Y * x);

    public static Vector2i operator *(int x, Vector2i v) => new Vector2i(v.X * x, v.Y * x);

    public static Vector2i operator /(Vector2i v, int x) => new Vector2i(v.X / x, v.Y / x);

    public static bool operator ==(Vector2i v1, Vector2i v2) => v1.Equals(v2);

    public static bool operator !=(Vector2i v1, Vector2i v2) => !v1.Equals(v2);

    public override string ToString() => $"[Vector2i] X({X}) Y({Y})";

    public override bool Equals(object obj) => ( obj is Vector2i ) && Equals((Vector2i)obj);

    public bool Equals(Vector2i other) => ( X == other.X ) && ( Y == other.Y );

    public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();

    public static explicit operator Vector2f(Vector2i v) => new Vector2f(v.X, v.Y);

    public static explicit operator Vector2u(Vector2i v) => new Vector2u((uint)v.X, (uint)v.Y);

    public int X;

    public int Y;
}

[StructLayout(LayoutKind.Sequential)]
public struct Vector2u : IEquatable<Vector2u>
{
    public Vector2u(uint x, uint y)
    {
        X = x;
        Y = y;
    }

    public static Vector2u operator -(Vector2u v1, Vector2u v2) => new Vector2u(v1.X - v2.X, v1.Y - v2.Y);

    public static Vector2u operator +(Vector2u v1, Vector2u v2) => new Vector2u(v1.X + v2.X, v1.Y + v2.Y);

    public static Vector2u operator *(Vector2u v, uint x) => new Vector2u(v.X * x, v.Y * x);

    public static Vector2u operator *(uint x, Vector2u v) => new Vector2u(v.X * x, v.Y * x);

    public static Vector2u operator /(Vector2u v, uint x) => new Vector2u(v.X / x, v.Y / x);

    public static bool operator ==(Vector2u v1, Vector2u v2) => v1.Equals(v2);

    public static bool operator !=(Vector2u v1, Vector2u v2) => !v1.Equals(v2);

    public override string ToString() => $"[Vector2u] X({X}) Y({Y})";

    public override bool Equals(object obj) => ( obj is Vector2u ) && Equals((Vector2u)obj);

    public bool Equals(Vector2u other) => ( X == other.X ) && ( Y == other.Y );

    public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();

    public static explicit operator Vector2i(Vector2u v) => new Vector2i((int)v.X, (int)v.Y);

    public static explicit operator Vector2f(Vector2u v) => new Vector2f(v.X, v.Y);

    public uint X;

    public uint Y;
}