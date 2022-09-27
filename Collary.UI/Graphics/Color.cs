using Collary.Native.SDL2;
using System;
using System.Collections.Generic;
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

    public Color() { }

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
}
