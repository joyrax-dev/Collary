﻿using Collary.Native.SDL2;
using Collary.UI.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.UI.Graphics;

public class Font : Base
{
    public static bool HasTTFInitialize { get; private set; } = false;
    public static int FontsCount { get; private set; } = 0;

    public static int DefaultFontSize { get; set; } = 8;

    public Font(string path_to_font, int pt_size)
    {
        this.Pointer = Font.OpenFont(path_to_font, pt_size);
    }

    public Font(string path_to_font)
    {
        this.Pointer = Font.OpenFont(path_to_font, Font.DefaultFontSize);
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
}