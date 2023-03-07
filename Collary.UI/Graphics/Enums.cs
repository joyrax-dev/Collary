using Collary.Native.SDL2;

namespace Collary.UI.Graphics;

public enum EncodingType
{
    Default,
    Utf8,
    Unicode
}

public enum FontHinting : int
{
    Normal        = TTF.TTF_HINTING_NORMAL,
    Light         = TTF.TTF_HINTING_LIGHT,
    LightSubpixel = TTF.TTF_HINTING_LIGHT_SUBPIXEL,
    Mono          = TTF.TTF_HINTING_MONO,
    None          = TTF.TTF_HINTING_NONE
}

public enum FontStyle : int
{
    Normal        = TTF.TTF_STYLE_NORMAL,
    Bold          = TTF.TTF_STYLE_BOLD,
    Italic        = TTF.TTF_STYLE_ITALIC,
    Underline     = TTF.TTF_STYLE_UNDERLINE,
    StrikeThrough = TTF.TTF_STYLE_STRIKETHROUGH
}