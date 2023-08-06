using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Core;

[StructLayout(LayoutKind.Sequential)]
public struct Vector2i : IEquatable<Vector2i>
{
    public int X;
    public int Y;

    public Vector2i(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Vector2i operator -(Vector2i v)
    {
        return new Vector2i(-v.X, -v.Y);
    }

    public static Vector2i operator -(Vector2i v1, Vector2i v2)
    {
        return new Vector2i(v1.X - v2.X, v1.Y - v2.Y);
    }

    public static Vector2i operator +(Vector2i v1, Vector2i v2)
    {
        return new Vector2i(v1.X + v2.X, v1.Y + v2.Y);
    }

    public static Vector2i operator *(Vector2i v, int x)
    {
        return new Vector2i(v.X * x, v.Y * x);
    }

    public static Vector2i operator *(int x, Vector2i v)
    {
        return new Vector2i(v.X * x, v.Y * x);
    }

    public static Vector2i operator /(Vector2i v, int x)
    {
        return new Vector2i(v.X / x, v.Y / x);
    }

    public static bool operator ==(Vector2i v1, Vector2i v2)
    {
        return v1.Equals(v2);
    }

    public static bool operator !=(Vector2i v1, Vector2i v2)
    {
        return !v1.Equals(v2);
    }

    public override string ToString()
    {
        return $"[Vector2i] X({X}) Y({Y})";
    }

    public override bool Equals(object obj)
    {
        return (obj is Vector2i) && Equals((Vector2i)obj);
    }

    public bool Equals(Vector2i other)
    {
        return (X == other.X) && (Y == other.Y);
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }

    public static explicit operator Vector2f(Vector2i v)
    {
        return new Vector2f(v.X, v.Y);
    }

    public static explicit operator Vector2u(Vector2i v)
    {
        return new Vector2u((uint)v.X, (uint)v.Y);
    }
}