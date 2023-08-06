using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Core;

[StructLayout(LayoutKind.Sequential)]
public struct Vector2u : IEquatable<Vector2u>
{
    public uint X;
    public uint Y;

    public Vector2u(uint x, uint y)
    {
        X = x;
        Y = y;
    }

    public static Vector2u operator -(Vector2u v1, Vector2u v2)
    {
        return new Vector2u(v1.X - v2.X, v1.Y - v2.Y);
    }

    public static Vector2u operator +(Vector2u v1, Vector2u v2)
    {
        return new Vector2u(v1.X + v2.X, v1.Y + v2.Y);
    }

    public static Vector2u operator *(Vector2u v, uint x)
    {
        return new Vector2u(v.X * x, v.Y * x);
    }

    public static Vector2u operator *(uint x, Vector2u v)
    {
        return new Vector2u(v.X * x, v.Y * x);
    }

    public static Vector2u operator /(Vector2u v, uint x)
    {
        return new Vector2u(v.X / x, v.Y / x);
    }

    public static bool operator ==(Vector2u v1, Vector2u v2)
    {
        return v1.Equals(v2);
    }

    public static bool operator !=(Vector2u v1, Vector2u v2)
    {
        return !v1.Equals(v2);
    }

    public override string ToString()
    {
        return $"[Vector2u] X({X}) Y({Y})";
    }

    public override bool Equals(object obj)
    {
        return (obj is Vector2u) && Equals((Vector2u)obj);
    }

    public bool Equals(Vector2u other)
    {
        return (X == other.X) && (Y == other.Y);
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }

    public static explicit operator Vector2i(Vector2u v)
    {
        return new Vector2i((int)v.X, (int)v.Y);
    }

    public static explicit operator Vector2f(Vector2u v)
    {
        return new Vector2f(v.X, v.Y);
    }
}