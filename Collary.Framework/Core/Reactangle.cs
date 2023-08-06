using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Core;

public class Rectangle
{
    public float X { get; set; } = 0;
    public float Y { get; set; } = 0;
    public float Width { get; set; } = 0;
    public float Height { get; set; } = 0;

    public Rectangle() { }

    public Rectangle(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public Rectangle(Vector2f position, Vector2f size) : this(position.X, position.Y, size.X, size.Y) { }

    public Rectangle(SDL.SDL_Rect rect) : this(rect.x, rect.y, rect.w, rect.h) { }

    #region SDL Support
    public SDL.SDL_Rect SDL_Rect
    {
        get
        {
            return new SDL.SDL_Rect()
            {
                x = (int)X,
                y = (int)Y,
                w = (int)Width,
                h = (int)Height
            };
        }
    }

    public SDL.SDL_FRect SDL_FRect
    {
        get
        {
            return new SDL.SDL_FRect()
            {
                x = X,
                y = Y,
                w = Width,
                h = Height
            };
        }
    }
    #endregion
}
