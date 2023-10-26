using Collary.Framework.Core;
using Collary.Framework.Windowing;
using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Graphics;

public class Renderer : Base
{
    #region Constructor
    public Renderer(Window window)
    {
        Point = CreateRenderer(window);
        Color = new Color(0, 0, 0, 255);
    }
    #endregion

    #region Create Renderer
    protected static nint CreateRenderer(Window window)
    {
        nint handle = SDL.SDL_CreateRenderer(window.Point, -1,
            SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE
            | SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

        if (handle == nint.Zero)
            throw new RendererCreationException();

        SDL.SDL_SetHint(SDL.SDL_HINT_RENDER_SCALE_QUALITY, "1");

        return handle;
    }
    #endregion

    #region Renderer System
    protected override void Destroy()
    {
        SDL.SDL_DestroyRenderer(Point);
    }
    #endregion

    #region Options
    public Font Font { get; set; }

    public Color Color
    {
        get
        {
            SDL.SDL_GetRenderDrawColor(Point, out byte r, out byte g, out byte b, out byte a);
            return new Color(r, g, b, a);
        }
        set
        {
            SDL.SDL_SetRenderDrawColor(Point, value.R, value.G, value.B, value.A);
        }
    }

    public Texture Target
    {
        get
        {
            nint target = SDL.SDL_GetRenderTarget(Point);

            if (target == nint.Zero)
                return null;

            return new Texture(target);
        }
        set
        {
            if (value == null)
                SDL.SDL_SetRenderTarget(Point, nint.Zero);
            else
                SDL.SDL_SetRenderTarget(Point, value.Point);
        }
    }
    #endregion

    #region Methods
    public void Clear()
    {
        SDL.SDL_RenderClear(Point);
    }

    public void Clear(Color color)
    {
        Color bkp_clr = Color;
        Color = color;
        Clear();
        Color = bkp_clr;
    }

    public void Present()
    {
        SDL.SDL_RenderPresent(Point);
    }
    #endregion

    #region Draw Methods
    public void Draw(Texture texture, Vector2i position)
    {
        Vector2i texture_size = texture.Size;
        SDL.SDL_Rect dst = new SDL.SDL_Rect()
        {
            x = position.X,
            y = position.Y,
            w = texture_size.X,
            h = texture_size.Y

        };
        SDL.SDL_RenderCopy(Point, texture.Point, nint.Zero, ref dst);
    }

    public void Draw(Texture texture, Rectangle source_rect, Rectangle dist_rect)
    {
        SDL.SDL_Rect src = source_rect.SDL_Rect;
        SDL.SDL_Rect dst = dist_rect.SDL_Rect;
        SDL.SDL_RenderCopy(Point, texture.Point, ref src, ref dst);
    }
    #endregion

    #region Draw Primitives
    public void DrawLine(Vector2i start, Vector2i end, bool anti_aliased = false)
    {
        if (!anti_aliased)
            SDL.SDL_RenderDrawLineF(Point, start.X, start.Y, end.X, end.Y);
        else
            GFX.aalineRGBA(Point,
                (short)start.X, (short)start.Y, (short)end.X, (short)end.Y,
                Color.R, Color.G, Color.B, Color.A);
    }

    public void DrawPoint(Vector2f point)
    {
        SDL.SDL_RenderDrawPointF(Point, point.X, point.Y);
    }

    public void DrawRect(Rectangle rect)
    {
        SDL.SDL_FRect dst = rect.SDL_FRect;
        SDL.SDL_RenderDrawRectF(Point, ref dst);
    }

    public void DrawRectRounded(Rectangle rect, short rad)
    {
        GFX.roundedRectangleRGBA(Point,
            (short)rect.X, (short)rect.Y, (short)rect.Width, (short)rect.Height,
            rad, Color.R, Color.G, Color.B, Color.A);
    }

    public void DrawFillRect(Rectangle rect)
    {
        SDL.SDL_FRect dst = rect.SDL_FRect;
        SDL.SDL_RenderFillRectF(Point, ref dst);
    }

    public void DrawFillRectRounded(Rectangle rect, short rad)
    {
        GFX.roundedBoxRGBA(Point,
            (short)rect.X, (short)rect.Y, (short)rect.Width, (short)rect.Height,
            rad, Color.R, Color.G, Color.B, Color.A);
    }

    public void DrawCircle(Vector2i position, short rad, bool anti_aliased = false)
    {
        if (!anti_aliased)
            GFX.circleRGBA(Point, (short)position.X, (short)position.Y,
            rad, Color.R, Color.G, Color.B, Color.A);
        else
            GFX.aacircleRGBA(Point, (short)position.X, (short)position.Y,
            rad, Color.R, Color.G, Color.B, Color.A);
    }

    public void DrawFillCircle(Vector2i position, short rad)
    {
        GFX.filledCircleRGBA(Point, (short)position.X, (short)position.Y,
            rad, Color.R, Color.G, Color.B, Color.A);
    }
    #endregion
}
