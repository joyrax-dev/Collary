using Collary.Native.SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Collary.UI.System;

public struct Rect
{
    public float X { get; set; } = 0;
    public float Y { get; set; } = 0;
    public float Width { get; set; } = 0;
    public float Height { get; set; } = 0;

    public Rect() { }

    public Rect(float x, float y, float width, float height)
    {
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
    }

    public Rect(Vector2 position, Vector2 size) : this(position.X, position.Y, size.X, size.Y) { }

    public Rect(SDL.SDL_Rect rect) : this(rect.x, rect.y, rect.w, rect.h) { }

    #region SDL Support
    public SDL.SDL_Rect SDL_Rect
    {
        get
        {
            return new SDL.SDL_Rect()
            {
                x = (int)this.X,
                y = (int)this.Y,
                w = (int)this.Width,
                h = (int)this.Height
            };
        }
    }

    public SDL.SDL_FRect SDL_FRect
    {
        get
        {
            return new SDL.SDL_FRect()
            {
                x = this.X,
                y = this.Y,
                w = this.Width,
                h = this.Height
            };
        }
    }
    #endregion
}
