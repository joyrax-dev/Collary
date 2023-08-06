using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Graphics;

public class Color : IEquatable<Color>
{
    #region RGBA
    public byte R { get; set; } = 0;
    public byte G { get; set; } = 0;
    public byte B { get; set; } = 0;
    public byte A { get; set; } = 0;
    #endregion

    #region HEX Support
    public string HEX
    {
        get => RgbaToHex(R, G, B, A);
        set
        {
            byte[] rgba = HexToRgba(value);
            R = rgba[0];
            G = rgba[1];
            B = rgba[2];
            A = rgba[3];
        }
    }
    #endregion

    #region SDL Support
    public SDL.SDL_Color SDL_Color
    {
        get
        {
            return new SDL.SDL_Color()
            {
                r = R,
                g = G,
                b = B,
                a = A
            };
        }
        set
        {
            R = value.r;
            G = value.g;
            B = value.b;
            A = value.a;
        }
    }
    #endregion

    #region Constructors
    public Color(byte r, byte g, byte b, byte a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public Color(byte r, byte g, byte b) : this(r, g, b, 255) { }

    public Color(Color color) : this(color.R, color.G, color.B, color.A) { }

    public Color(SDL.SDL_Color color) : this(color.r, color.g, color.b, color.a) { }

    public Color(string hex)
    {
        HEX = hex;
    }

    public Color() { }
    #endregion

    #region Utils
    public static byte[] HexToRgba(string hex)
    {
        if (hex.IndexOf('#') == -1)
            throw new Exception("Parse error hex string, no indexed '#'");

        hex = hex.Replace("#", "");

        int r = 0;
        int g = 0;
        int b = 0;
        int a = 0;

        if (hex.Length == 8)
        {
            //#RRGGBBAA
            r = int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            g = int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            b = int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
            a = int.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);
        }

        if (hex.Length == 6)
        {
            //#RRGGBB
            r = int.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            g = int.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            b = int.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
            a = 255;
        }

        return new byte[] { ((byte)r), ((byte)g), ((byte)b), ((byte)a) };
    }

    public static string RgbaToHex(byte r, byte g, byte b, byte a)
    {
        return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", r, g, b, a);
    }
    #endregion

    #nullable disable
    public bool Equals(Color other)
    {
        if (R == other.R && G == other.G && B == other.B && A == other.A) return true;
        else return false;
    }
}
