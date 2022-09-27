using Collary.Native.SDL2;
using Collary.UI.System;
using Collary.UI.Windowing;
using System;
using System.Numerics;

namespace Collary.UI.Graphics;

public class Renderer : Base
{
    public Renderer(Window window)
    {
        IntPtr handle = SDL.SDL_CreateRenderer(window.Pointer, -1,
            SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE
            | SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

        if (handle == IntPtr.Zero)
            throw new RendererCreationException();

        this.Pointer = handle;
    }

    protected override void Destroy()
    {
        SDL.SDL_DestroyRenderer(this.Pointer);
    }

    public void Clear()
    {
        SDL.SDL_RenderClear(this.Pointer);
    }

    public void Clear(Color color)
    {
        Color backup_color = this.Color;
        this.Color = color;
        this.Clear();
        this.Color = backup_color;
    }

    public void Present()
    {
        SDL.SDL_RenderPresent(this.Pointer);
    }

    public void Draw(Texture texture, Vector2 position)
    {
        Vector2 texture_size = texture.Size;
        SDL.SDL_Rect dst = new SDL.SDL_Rect()
        {
            x = ((int)position.X),
            y = ((int)position.Y),
            w = ((int)texture_size.X),
            h = ((int)texture_size.Y)

        };
        SDL.SDL_RenderCopy(this.Pointer, texture.Pointer, IntPtr.Zero, ref dst);
    }

    public void Draw(Texture texture, Rect source_rect, Rect dist_rect)
    {
        SDL.SDL_Rect src = source_rect.SDL_Rect;
        SDL.SDL_Rect dst = dist_rect.SDL_Rect;
        SDL.SDL_RenderCopy(this.Pointer, texture.Pointer, ref src, ref dst);
    }

    public void DrawLine(Vector2 start, Vector2 end)
    {
        SDL.SDL_RenderDrawLineF(this.Pointer, start.X, start.Y, end.X, end.Y);
    }

    public void DrawPoint(Vector2 point)
    {
        SDL.SDL_RenderDrawPointF(this.Pointer, point.X, point.Y);
    }

    public void DrawRect(Rect rect)
    {
        SDL.SDL_FRect dst = rect.SDL_FRect;
        SDL.SDL_RenderDrawRectF(this.Pointer, ref dst);
    }

    public void DrawFillRect(Rect rect)
    {
        SDL.SDL_FRect dst = rect.SDL_FRect;
        SDL.SDL_RenderFillRectF(this.Pointer, ref dst);
    }

    public Color Color
    {
        get
        {
            SDL.SDL_GetRenderDrawColor(this.Pointer, out byte r, out byte g, out byte b, out byte a);
            return new Color(r, g, b, a);
        }
        set
        {
            SDL.SDL_SetRenderDrawColor(this.Pointer, value.R, value.G, value.B, value.A);
        }
    }

    public Texture Target
    {
        get
        {
            IntPtr target = SDL.SDL_GetRenderTarget(this.Pointer);

            if (target == IntPtr.Zero)
                return null;

            return new Texture(target);
        }
        set
        {
            if (value == null)
                SDL.SDL_SetRenderTarget(this.Pointer, IntPtr.Zero);
            else
                SDL.SDL_SetRenderTarget(this.Pointer, value.Pointer);
        }
    }
}
