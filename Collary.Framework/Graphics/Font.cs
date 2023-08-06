using Collary.Framework.Core;
using Collary.Framework.Graphics.Text;
using Collary.Native.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Graphics;

public class Font : Base
{
    #region Variables
    public static int DefaultFontSize { get; set; } = 16;
    protected int FontSize { get; set; }
    #endregion
    #region Constructors
    public Font(string path_to_font, int pt_size)
    {
        Point = OpenFont(path_to_font, pt_size);
        FontSize = pt_size;
    }

    public Font(string path_to_font)
    {
        Point = OpenFont(path_to_font, DefaultFontSize);
        FontSize = DefaultFontSize;
    }
    #endregion
    #region Opened Font
    protected static nint OpenFont(string path_to_font, int pt_size)
    {
        TTFInitialize();

        nint handle = TTF.TTF_OpenFont(path_to_font, pt_size);

        if (handle == nint.Zero)
            throw new OpenFontException(path_to_font);

        FontsCount++;
        return handle;
    }
    #endregion
    #region Methods
    public Vector2i TextSize(string text, EncodingType encoding = EncodingType.Default)
    {
        Vector2i size;
        if (encoding == EncodingType.Default)
        {
            TTF.TTF_SizeText(Point, text, out int w, out int h);
            size = new Vector2i(w, h);
        }
        else if (encoding == EncodingType.Utf8)
        {
            TTF.TTF_SizeUTF8(Point, text, out int w, out int h);
            size = new Vector2i(w, h);
        }
        else
        {
            TTF.TTF_SizeUNICODE(Point, text, out int w, out int h);
            size = new Vector2i(w, h);
        }

        return size;
    }
    #endregion
    #region Options
    public int Size
    {
        get
        {
            return FontSize;
        }
        set
        {
            TTF.TTF_SetFontSize(Point, value);
            FontSize = value;
        }
    }

    public int Height
    {
        get
        {
            return TTF.TTF_FontHeight(Point);
        }
    }

    public FontStyle Style
    {
        get
        {
            return (FontStyle)TTF.TTF_GetFontStyle(Point);
        }
        set
        {
            TTF.TTF_SetFontStyle(Point, (int)value);
        }
    }

    public int Outline
    {
        get
        {
            return TTF.TTF_GetFontOutline(Point);
        }
        set
        {
            TTF.TTF_SetFontOutline(Point, value);
        }
    }

    public FontHinting Hinting
    {
        get
        {
            return (FontHinting)TTF.TTF_GetFontHinting(Point);
        }
        set
        {
            TTF.TTF_SetFontHinting(Point, (int)value);
        }
    }

    public string FamilyName
    {
        get
        {
            return TTF.TTF_FontFaceFamilyName(Point);
        }
    }
    #endregion
    #region TTF System
    protected static bool IsTTFInitialize { get; private set; } = false;
    protected static int FontsCount { get; private set; } = 0;

    protected static void TTFInitialize()
    {
        if (!IsTTFInitialize)
            if (TTF.TTF_Init() != 0)
                throw new TTFInitializationException();
            else
                IsTTFInitialize = true;
    }

    protected override void Destroy()
    {
        TTF.TTF_CloseFont(Point);
        FontsCount--;

        if (FontsCount <= 0)
        {
            TTF.TTF_Quit();
            IsTTFInitialize = false;
        }
    }
    #endregion
}
