using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Graphics.Text;

public class TextFactory
{
    protected Renderer RefRen { get; set; }

    public Font Font { get; set; }

    public bool Wrapped { get; set; } = false;
    public uint WrapLength { get; set; }
    public DrawTextType DrawTextType { get; set; } = DrawTextType.Solid;
    public EncodingType Encoding { get; set; } = EncodingType.Default;

    public Color Forceground { get; set; } = new Color(255, 255, 255);
    public Color Background { get; set; } = new Color(0, 0, 0, 0);

    public TextFactory(Renderer ren, Font font)
    {
        RefRen = ren;
        Font = font;
    }

    public Texture Create(string text)
    {
        if (Font == null)
            throw new NullReferenceException("Font null reference");

        nint surface;

        if (Wrapped)
        {
            if (Encoding == EncodingType.Utf8)
            {
                if (DrawTextType == DrawTextType.Solid)
                {
                    surface = TTF.TTF_RenderUTF8_Solid_Wrapped(Font.Point, text, Forceground.SDL_Color, WrapLength);
                }
                else if (DrawTextType == DrawTextType.Shaded)
                {
                    surface = TTF.TTF_RenderUTF8_Shaded_Wrapped(Font.Point, text, Forceground.SDL_Color, Background.SDL_Color, WrapLength);
                }
                else
                {
                    surface = TTF.TTF_RenderUTF8_Blended_Wrapped(Font.Point, text, Forceground.SDL_Color, WrapLength);
                }
            }
            else if (Encoding == EncodingType.Unicode)
            {
                if (DrawTextType == DrawTextType.Solid)
                {
                    surface = TTF.TTF_RenderUNICODE_Solid_Wrapped(Font.Point, text, Forceground.SDL_Color, WrapLength);
                }
                else if (DrawTextType == DrawTextType.Shaded)
                {
                    surface = TTF.TTF_RenderUNICODE_Shaded_Wrapped(Font.Point, text, Forceground.SDL_Color, Background.SDL_Color, WrapLength);
                }
                else
                {
                    surface = TTF.TTF_RenderUNICODE_Blended_Wrapped(Font.Point, text, Forceground.SDL_Color, WrapLength);
                }
            }
            else
            {
                if (DrawTextType == DrawTextType.Solid)
                {
                    surface = TTF.TTF_RenderText_Solid_Wrapped(Font.Point, text, Forceground.SDL_Color, WrapLength);
                }
                else if (DrawTextType == DrawTextType.Shaded)
                {
                    surface = TTF.TTF_RenderText_Shaded_Wrapped(Font.Point, text, Forceground.SDL_Color, Background.SDL_Color, WrapLength);
                }
                else
                {
                    surface = TTF.TTF_RenderText_Blended_Wrapped(Font.Point, text, Forceground.SDL_Color, WrapLength);
                }
            }
        }
        else
        {
            if (Encoding == EncodingType.Utf8)
            {
                if (DrawTextType == DrawTextType.Solid)
                {
                    surface = TTF.TTF_RenderUTF8_Solid(Font.Point, text, Forceground.SDL_Color);
                }
                else if (DrawTextType == DrawTextType.Shaded)
                {
                    surface = TTF.TTF_RenderUTF8_Shaded(Font.Point, text, Forceground.SDL_Color, Background.SDL_Color);
                }
                else
                {
                    surface = TTF.TTF_RenderUTF8_Blended(Font.Point, text, Forceground.SDL_Color);
                }
            }
            else if (Encoding == EncodingType.Unicode)
            {
                if (DrawTextType == DrawTextType.Solid)
                {
                    surface = TTF.TTF_RenderUNICODE_Solid(Font.Point, text, Forceground.SDL_Color);
                }
                else if (DrawTextType == DrawTextType.Shaded)
                {
                    surface = TTF.TTF_RenderUNICODE_Shaded(Font.Point, text, Forceground.SDL_Color, Background.SDL_Color);
                }
                else
                {
                    surface = TTF.TTF_RenderUNICODE_Blended(Font.Point, text, Forceground.SDL_Color);
                }
            }
            else
            {
                if (DrawTextType == DrawTextType.Solid)
                {
                    surface = TTF.TTF_RenderText_Solid(Font.Point, text, Forceground.SDL_Color);
                }
                else if (DrawTextType == DrawTextType.Shaded)
                {
                    surface = TTF.TTF_RenderText_Shaded(Font.Point, text, Forceground.SDL_Color, Background.SDL_Color);
                }
                else
                {
                    surface = TTF.TTF_RenderText_Blended(Font.Point, text, Forceground.SDL_Color);
                }
            }
        }

        Texture tex = new Texture(SDL.SDL_CreateTextureFromSurface(RefRen.Point, surface));
        SDL.SDL_FreeSurface(surface);

        return tex;
    }
}