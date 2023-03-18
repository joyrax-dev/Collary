using Collary.Native.SDL2;
using Collary.UI.System;
using Collary.UI.Windowing;
using System;
using System.Numerics;

namespace Collary.UI.Graphics;

public class TextFactory
{
    protected Renderer RefRen { get; set; }

    public Font Font { get; set; }

    public bool Wrapped { get; set; } = false;
    public uint WrapLength { get; set; }
    public DrawType DrawType { get; set;} = DrawType.Solid;
    public EncodingType Encoding { get; set; } = EncodingType.Default;

    public Color Forceground { get; set; } = new Color(255, 255, 255);
    public Color Background { get; set; } = new Color(0, 0, 0, 0);
    
    public TextFactory(Renderer ren, Font font)
    {
        this.RefRen = ren;
        this.Font = font;
    }

    public Texture Create(string text)
    {
        if (this.Font == null)
            throw new NullReferenceException("Font null reference");
        
        nint surface;

        if (this.Wrapped)
        {
            if (this.Encoding == EncodingType.Utf8)
            {
                if (this.DrawType == DrawType.Solid)
                {
                    surface = TTF.TTF_RenderUTF8_Solid_Wrapped(this.Font.Pointer, text, this.Forceground.SDL_Color, this.WrapLength);
                }
                else if (this.DrawType == DrawType.Shaded)
                {
                    surface = TTF.TTF_RenderUTF8_Shaded_Wrapped(this.Font.Pointer, text, this.Forceground.SDL_Color, this.Background.SDL_Color, this.WrapLength);
                }
                else
                {
                    surface = TTF.TTF_RenderUTF8_Blended_Wrapped(this.Font.Pointer, text, this.Forceground.SDL_Color, this.WrapLength);
                }
            }
            else if (this.Encoding == EncodingType.Unicode)
            {
                if (this.DrawType == DrawType.Solid)
                {
                    surface = TTF.TTF_RenderUNICODE_Solid_Wrapped(this.Font.Pointer, text, this.Forceground.SDL_Color, this.WrapLength);
                }
                else if (this.DrawType == DrawType.Shaded)
                {
                    surface = TTF.TTF_RenderUNICODE_Shaded_Wrapped(this.Font.Pointer, text, this.Forceground.SDL_Color, this.Background.SDL_Color, this.WrapLength);
                }
                else
                {
                    surface = TTF.TTF_RenderUNICODE_Blended_Wrapped(this.Font.Pointer, text, this.Forceground.SDL_Color, this.WrapLength);
                }
            }
            else
            {
                if (this.DrawType == DrawType.Solid)
                {
                    surface = TTF.TTF_RenderText_Solid_Wrapped(this.Font.Pointer, text, this.Forceground.SDL_Color, this.WrapLength);
                }
                else if (this.DrawType == DrawType.Shaded)
                {
                    surface = TTF.TTF_RenderText_Shaded_Wrapped(this.Font.Pointer, text, this.Forceground.SDL_Color, this.Background.SDL_Color, this.WrapLength);
                }
                else
                {
                    surface = TTF.TTF_RenderText_Blended_Wrapped(this.Font.Pointer, text, this.Forceground.SDL_Color, this.WrapLength);
                }
            }
        }
        else
        {
            if (this.Encoding == EncodingType.Utf8)
            {
                if (this.DrawType == DrawType.Solid)
                {
                    surface = TTF.TTF_RenderUTF8_Solid(this.Font.Pointer, text, this.Forceground.SDL_Color);
                }
                else if (this.DrawType == DrawType.Shaded)
                {
                    surface = TTF.TTF_RenderUTF8_Shaded(this.Font.Pointer, text, this.Forceground.SDL_Color, this.Background.SDL_Color);
                }
                else
                {
                    surface = TTF.TTF_RenderUTF8_Blended(this.Font.Pointer, text, this.Forceground.SDL_Color);
                }
            }
            else if (this.Encoding == EncodingType.Unicode)
            {
                if (this.DrawType == DrawType.Solid)
                {
                    surface = TTF.TTF_RenderUNICODE_Solid(this.Font.Pointer, text, this.Forceground.SDL_Color);
                }
                else if (this.DrawType == DrawType.Shaded)
                {
                    surface = TTF.TTF_RenderUNICODE_Shaded(this.Font.Pointer, text, this.Forceground.SDL_Color, this.Background.SDL_Color);
                }
                else
                {
                    surface = TTF.TTF_RenderUNICODE_Blended(this.Font.Pointer, text, this.Forceground.SDL_Color);
                }
            }
            else
            {
                if (this.DrawType == DrawType.Solid)
                {
                    surface = TTF.TTF_RenderText_Solid(this.Font.Pointer, text, this.Forceground.SDL_Color);
                }
                else if (this.DrawType == DrawType.Shaded)
                {
                    surface = TTF.TTF_RenderText_Shaded(this.Font.Pointer, text, this.Forceground.SDL_Color, this.Background.SDL_Color);
                }
                else
                {
                    surface = TTF.TTF_RenderText_Blended(this.Font.Pointer, text, this.Forceground.SDL_Color);
                }
            }
        }

        Texture tex = new Texture(SDL.SDL_CreateTextureFromSurface(this.RefRen.Pointer, surface));
        SDL.SDL_FreeSurface(surface);

        return tex;
    }
}