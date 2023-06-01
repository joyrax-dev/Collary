using Collary.Native.SDL2;
using Collary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Graphics;

public class Font : Base
{
    protected static bool HasTTFInitialize { get; private set; } = false;
    protected static int FontsCount { get; private set; } = 0;

    public static int DefaultFontSize { get; set; } = 16;

    protected int FontSize { get; set; }

    public Font(string path_to_font, int pt_size)
    {
        this.Pointer = Font.OpenFont(path_to_font, pt_size);
        this.FontSize = pt_size;
    }

    public Font(string path_to_font)
    {
        this.Pointer = Font.OpenFont(path_to_font, Font.DefaultFontSize);
        this.FontSize = Font.DefaultFontSize;
    }

    protected static IntPtr OpenFont(string path_to_font, int pt_size)
    {
        if (!Font.HasTTFInitialize)
            if (TTF.TTF_Init() != 0)
                throw new TTFInitializationException();
            else
                Font.HasTTFInitialize = true;

        IntPtr handle = TTF.TTF_OpenFont(path_to_font, pt_size);

        if (handle == IntPtr.Zero)
            throw new OpenFontException(path_to_font);

        Font.FontsCount++;
        return handle;
    }

    public Vector2i TextSize(string text, EncodingType encoding = EncodingType.Default)
    {
        Vector2i size;
        if (encoding == EncodingType.Default)
        {
            TTF.TTF_SizeText(this.Pointer, text, out int w, out int h);
            size = new Vector2i(w, h);
        }
        else if (encoding == EncodingType.Utf8)
        {
            TTF.TTF_SizeUTF8(this.Pointer, text, out int w, out int h);
            size = new Vector2i(w, h);
        }
        else
        {
            TTF.TTF_SizeUNICODE(this.Pointer, text, out int w, out int h);
            size = new Vector2i(w, h);
        }

        return size;
    }

    protected override void Destroy()
    {
        TTF.TTF_CloseFont(this.Pointer);
        Font.FontsCount--;

        if (Font.FontsCount <= 0)
        {
            TTF.TTF_Quit();
            Font.HasTTFInitialize = false;
        }
    }

    public int Size
    {
        get
        {
            return this.FontSize;
        }
        set
        {
            TTF.TTF_SetFontSize(this.Pointer, value);
            this.FontSize = value;
        }
    }

    public int Height
    {
        get
        {
            return TTF.TTF_FontHeight(this.Pointer);
        }
    }

    public FontStyle Style
    {
        get
        {
            return ((FontStyle)TTF.TTF_GetFontStyle(this.Pointer));
        }
        set
        {
            TTF.TTF_SetFontStyle(this.Pointer, ((int)value));
        }
    }

    public int Outline
    {
        get
        {
            return TTF.TTF_GetFontOutline(this.Pointer);
        }
        set
        {
            TTF.TTF_SetFontOutline(this.Pointer, value);
        }
    }

    public FontHinting Hinting
    {
        get
        {
            return ((FontHinting)TTF.TTF_GetFontHinting(this.Pointer));
        }
        set
        {
            TTF.TTF_SetFontHinting(this.Pointer, ((int)value));
        }
    }

    public string FamilyName
    {
        get
        {
            return TTF.TTF_FontFaceFamilyName(this.Pointer);
        }
    }
}
