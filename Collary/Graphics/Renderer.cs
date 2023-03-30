using Collary.Native.SDL2;
using Collary.Core;
using Collary.Windowing;
using System;
using System.Numerics;

namespace Collary.Graphics;

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
        this.Color = new Color(0, 0, 0, 255);
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

    public void DrawLine(Vector2 start, Vector2 end, bool anti_aliased = false)
    {
        if (!anti_aliased)
            SDL.SDL_RenderDrawLineF(this.Pointer, start.X, start.Y, end.X, end.Y);
        else
            GFX.aalineRGBA(this.Pointer,
                ((short)start.X), ((short)start.Y), ((short)end.X), ((short)end.Y),
                this.Color.R, this.Color.G, this.Color.B, this.Color.A);
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

    public void DrawRectRounded(Rect rect, short rad)
    {
        GFX.roundedRectangleRGBA(this.Pointer, 
            ((short)rect.X), ((short)rect.Y), ((short)rect.Width), ((short)rect.Height), 
            rad, this.Color.R, this.Color.G, this.Color.B, this.Color.A);
    }

    public void DrawFillRect(Rect rect)
    {
        SDL.SDL_FRect dst = rect.SDL_FRect;
        SDL.SDL_RenderFillRectF(this.Pointer, ref dst);
    }

    public void DrawFillRectRounded(Rect rect, short rad)
    {
        GFX.roundedBoxRGBA(this.Pointer, 
            ((short)rect.X), ((short)rect.Y), ((short)rect.Width), ((short)rect.Height), 
            rad, this.Color.R, this.Color.G, this.Color.B, this.Color.A);
    }

    public void DrawCircle(Vector2 position, short rad, bool anti_aliased = false)
    {
        if (!anti_aliased)
            GFX.circleRGBA(this.Pointer, ((short)position.X), ((short)position.Y),
            rad, this.Color.R, this.Color.G, this.Color.B, this.Color.A);
        else
            GFX.aacircleRGBA(this.Pointer, ((short)position.X), ((short)position.Y),
            rad, this.Color.R, this.Color.G, this.Color.B, this.Color.A);
    }

    public void DrawFillCircle(Vector2 position, short rad)
    {
        GFX.filledCircleRGBA(this.Pointer, ((short)position.X), ((short)position.Y),
            rad, this.Color.R, this.Color.G, this.Color.B, this.Color.A);
    }

    public Texture DrawTextSolid(string text, EncodingType encoding = EncodingType.Default)
    {
        nint surface;

        if (this.Font == null)
            throw new NullReferenceException("Font null reference");

        if (encoding == EncodingType.Unicode)
        {
            surface = TTF.TTF_RenderUNICODE_Solid(this.Font.Pointer, text, this.Color.SDL_Color);
        }
        else if (encoding == EncodingType.Utf8)
        {
            surface = TTF.TTF_RenderUTF8_Solid(this.Font.Pointer, text, this.Color.SDL_Color);
        }
        else
        {
            surface = TTF.TTF_RenderText_Solid(this.Font.Pointer, text, this.Color.SDL_Color);
        }

        Texture tex = new Texture(SDL.SDL_CreateTextureFromSurface(this.Pointer, surface));
        SDL.SDL_FreeSurface(surface);

        return tex;
    }

    public Texture DrawTextSolidWrapped(string text, int wrap_length, EncodingType encoding = EncodingType.Default)
    {
        nint surface;

        if (this.Font == null)
            throw new NullReferenceException("Font null reference");

        if (encoding == EncodingType.Unicode)
        {
            surface = TTF.TTF_RenderUNICODE_Solid_Wrapped(this.Font.Pointer, text, this.Color.SDL_Color, (uint)wrap_length);
        }
        else if (encoding == EncodingType.Utf8)
        {
            surface = TTF.TTF_RenderUTF8_Solid_Wrapped(this.Font.Pointer, text, this.Color.SDL_Color, (uint)wrap_length);
        }
        else
        {
            surface = TTF.TTF_RenderText_Solid_Wrapped(this.Font.Pointer, text, this.Color.SDL_Color, (uint)wrap_length);
        }

        Texture tex = new Texture(SDL.SDL_CreateTextureFromSurface(this.Pointer, surface));
        SDL.SDL_FreeSurface(surface);

        return tex;
    }

    public Texture DrawTextShaded(string text, Color bg, EncodingType encoding = EncodingType.Default)
    {
        nint surface;

        if (this.Font == null)
            throw new NullReferenceException("Font null reference");

        if (encoding == EncodingType.Unicode)
        {
            surface = TTF.TTF_RenderUNICODE_Shaded(this.Font.Pointer, text, this.Color.SDL_Color, bg.SDL_Color);
        }
        else if (encoding == EncodingType.Utf8)
        {
            surface = TTF.TTF_RenderUTF8_Shaded(this.Font.Pointer, text, this.Color.SDL_Color, bg.SDL_Color);
        }
        else
        {
            surface = TTF.TTF_RenderText_Shaded(this.Font.Pointer, text, this.Color.SDL_Color, bg.SDL_Color);
        }

        Texture tex = new Texture(SDL.SDL_CreateTextureFromSurface(this.Pointer, surface));
        SDL.SDL_FreeSurface(surface);

        return tex;
    }

    public Texture DrawTextShadedWrapped(string text, int wrap_length, Color bg, EncodingType encoding = EncodingType.Default)
    {
        nint surface;

        if (this.Font == null)
            throw new NullReferenceException("Font null reference");

        if (encoding == EncodingType.Unicode)
        {
            surface = TTF.TTF_RenderUNICODE_Shaded_Wrapped(this.Font.Pointer, text, this.Color.SDL_Color, bg.SDL_Color, (uint)wrap_length);
        }
        else if (encoding == EncodingType.Utf8)
        {
            surface = TTF.TTF_RenderUTF8_Shaded_Wrapped(this.Font.Pointer, text, this.Color.SDL_Color, bg.SDL_Color, (uint)wrap_length);
        }
        else
        {
            surface = TTF.TTF_RenderText_Shaded_Wrapped(this.Font.Pointer, text, this.Color.SDL_Color, bg.SDL_Color, (uint)wrap_length);
        }

        Texture tex = new Texture(SDL.SDL_CreateTextureFromSurface(this.Pointer, surface));
        SDL.SDL_FreeSurface(surface);

        return tex;
    }

    public Texture DrawTextBlended(string text, EncodingType encoding = EncodingType.Default)
    {
        nint surface;

        if (this.Font == null)
            throw new NullReferenceException("Font null reference");

        if (encoding == EncodingType.Unicode)
        {
            surface = TTF.TTF_RenderUNICODE_Blended(this.Font.Pointer, text, this.Color.SDL_Color);
        }
        else if (encoding == EncodingType.Utf8)
        {
            surface = TTF.TTF_RenderUTF8_Blended(this.Font.Pointer, text, this.Color.SDL_Color);
        }
        else
        {
            surface = TTF.TTF_RenderText_Blended(this.Font.Pointer, text, this.Color.SDL_Color);
        }

        Texture tex = new Texture(SDL.SDL_CreateTextureFromSurface(this.Pointer, surface));
        SDL.SDL_FreeSurface(surface);

        return tex;
    }

    public Texture DrawTextBlendedWrapped(string text, int wrap_length, EncodingType encoding = EncodingType.Default)
    {
        nint surface;

        if (this.Font == null)
            throw new NullReferenceException("Font null reference");

        if (encoding == EncodingType.Unicode)
        {
            surface = TTF.TTF_RenderUNICODE_Blended_Wrapped(this.Font.Pointer, text, this.Color.SDL_Color, (uint)wrap_length);
        }
        else if (encoding == EncodingType.Utf8)
        {
            surface = TTF.TTF_RenderUTF8_Blended_Wrapped(this.Font.Pointer, text, this.Color.SDL_Color, (uint)wrap_length);
        }
        else
        {
            surface = TTF.TTF_RenderText_Blended_Wrapped(this.Font.Pointer, text, this.Color.SDL_Color, (uint)wrap_length);
        }

        Texture tex = new Texture(SDL.SDL_CreateTextureFromSurface(this.Pointer, surface));
        SDL.SDL_FreeSurface(surface);

        return tex;
    }

    public Font Font { get; set; }

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
