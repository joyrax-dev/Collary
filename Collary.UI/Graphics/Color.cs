using Collary.Native.SDL2;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.UI.Graphics;

public class Color
{
    public byte R { get; set; } = 0;
    public byte G { get; set; } = 0;
    public byte B { get; set; } = 0;
    public byte A { get; set; } = 0;

    // !!!HEX BETA FUNCTIONS!!!
    public string HEX 
    { 
        get => Color.RgbaToHex(this.R, this.G, this.B, this.A);
        set {
            byte[] rgba = Color.HexToRgba(value);
            this.R = rgba[0];
            this.G = rgba[1];
            this.B = rgba[2];
            this.A = rgba[3];
        }
    }

    public Color() { }

    public Color(string hex) 
    {
        this.HEX = hex;
    }

    public Color(byte r, byte g, byte b) : this(r, g, b, 255) { }

    public Color(byte r, byte g, byte b, byte a)
    {
        this.R = r;
        this.G = g;
        this.B = b;
        this.A = a;
    }

    public Color(Color color) : this(color.R, color.G, color.B, color.A) { }

    #region SDL Support
    public Color(SDL.SDL_Color color) : this(color.r, color.g, color.b, color.a) { }

    public SDL.SDL_Color SDL_Color
    {
        get
        {
            return new SDL.SDL_Color()
            {
                r = this.R,
                g = this.G,
                b = this.B,
                a = this.A
            };
        }
    }
    #endregion

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

        return new byte[] {((byte)r), ((byte)g), ((byte)b), ((byte)a)};
    }

    public static string RgbaToHex(byte r, byte g, byte b, byte a)
    {
        return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", r, g, b, a);
    } 
}
