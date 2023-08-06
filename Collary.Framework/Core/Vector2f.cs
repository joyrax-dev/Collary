using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Core;

[StructLayout(LayoutKind.Sequential)]
public struct Vector2f : IEquatable<Vector2f>
{
    public float X;
    public float Y;

    public Vector2f(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static Vector2f operator -(Vector2f v)
    {
        return new Vector2f(-v.X, -v.Y);
    }

    public static Vector2f operator -(Vector2f v1, Vector2f v2)
    {
        return new Vector2f(v1.X - v2.X, v1.Y - v2.Y);
    }

    public static Vector2f operator +(Vector2f v1, Vector2f v2)
    {
        return new Vector2f(v1.X + v2.X, v1.Y + v2.Y);
    }

    public static Vector2f operator *(Vector2f v, float x)
    {
        return new Vector2f(v.X * x, v.Y * x);
    }

    public static Vector2f operator *(float x, Vector2f v)
    {
        return new Vector2f(v.X * x, v.Y * x);
    }

    public static Vector2f operator /(Vector2f v, float x)
    {
        return new Vector2f(v.X / x, v.Y / x);
    }

    public static bool operator ==(Vector2f v1, Vector2f v2)
    {
        return v1.Equals(v2);
    }

    public static bool operator !=(Vector2f v1, Vector2f v2)
    {
        return !v1.Equals(v2);
    }

    public override string ToString()
    {
        return $"[Vector2f] X({X}) Y({Y})";
    }

    public override bool Equals(object obj)
    {
        return (obj is Vector2f) && Equals((Vector2f)obj);
    }

    public bool Equals(Vector2f other)
    {
        return (X == other.X) && (Y == other.Y);
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }

    public static explicit operator Vector2i(Vector2f v)
    {
        return new Vector2i((int)v.X, (int)v.Y);
    }

    public static explicit operator Vector2u(Vector2f v)
    {
        return new Vector2u((uint)v.X, (uint)v.Y);
    }
}
